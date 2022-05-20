using AEMS.Data.Entities;
using AEMS.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AEMS.Data.EF
{
    public class AppDbContext : DbContext
    {
        #region Fields

        /// <summary>
        /// The authentication session
        /// </summary>
        private readonly IAuthSession _authSession;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        public AppDbContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="authSession">The authentication session.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options, IAuthSession authSession) : base(options)
        {
            Database.EnsureCreated();
            _authSession = authSession;
        }

        #endregion

        #region DbSet

        public DbSet<Area> Area { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<EquipmentCategory> EquipmentCategory { get; set; }
        public DbSet<EquipmentLog> EquipmentLog { get; set; }
        public DbSet<Photo> Photo { get; set; }

        #endregion

        #region OnConfiguring

        /// <summary>
        /// <para>
        /// Override this method to configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// The base implementation does nothing.
        /// </para>
        /// <para>
        /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        /// the options have already been set, and skip some or all of the logic in
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        /// </para>
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(AppSettingValues.SqlConnectionString, builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            }
            base.OnConfiguring(optionsBuilder);
        }

        #endregion

        #region OnModelCreating

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IHasSoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddSoftDeleteQueryFilter();

                }
            }
            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region SaveSaveChanges

        /// <summary>
        /// <para>
        /// Saves all changes made in this context to the database.
        /// </para>
        /// <para>
        /// This method will automatically call <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges" /> to discover any
        /// changes to entity instances before saving to the underlying database. This can be disabled via
        /// <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled" />.
        /// </para>
        /// <para>
        /// Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        /// that any asynchronous operations have completed before calling another method on this context.
        /// </para>
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indicates whether <see cref="M:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges" /> is called after the changes have
        /// been sent successfully to the database.</param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous save operation. The task result contains the
        /// number of state entries written to the database.
        /// </returns>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Called when [before saving].
        /// </summary>
        private void OnBeforeSaving()
        {
            ChangeTracker.DetectChanges();

            var userId = _authSession.UserSession != null ? _authSession.UserSession.Id : Guid.Empty;

            var entitiesTrackingChanged = ChangeTracker.Entries().Where(e => e.State == EntityState.Added ||
                                                                             e.State == EntityState.Modified ||
                                                                             e.State == EntityState.Deleted);

            foreach (var entry in entitiesTrackingChanged)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity is BaseEntity addBaseEntity)
                        {
                            addBaseEntity.CreatedAt = DateTimeCountry.DateTimeNow;
                            addBaseEntity.CreatedBy = userId;
                        }
                        break;

                    case EntityState.Deleted:
                        if (entry.Entity is IHasSoftDelete deleteEntry)
                        {
                            entry.State = EntityState.Modified;
                            deleteEntry.IsDeleted = true;
                        }
                        break;

                    case EntityState.Modified:
                        if (entry.Entity is BaseEntity modifyBaseEntity)
                        {
                            modifyBaseEntity.LastModifiedAt = DateTimeCountry.DateTimeNow;
                            modifyBaseEntity.ModifedBy = userId;
                        }
                        break;
                }
            }
        }

        #endregion
    }

    #region HasSoftDelete Config

    public static class SoftDeleteQueryExtension
    {
        /// <summary>
        /// Adds the soft delete query filter.
        /// </summary>
        /// <param name="entityData">The entity data.</param>
        public static void AddSoftDeleteQueryFilter(this IMutableEntityType entityData)
        {
            var methodToCall = typeof(SoftDeleteQueryExtension).GetMethod(nameof(GetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(entityData.ClrType);

            var filter = methodToCall.Invoke(null, new object[] { });

            entityData.SetQueryFilter((LambdaExpression)filter);

            entityData.AddIndex(entityData.
                 FindProperty(nameof(IHasSoftDelete.IsDeleted)));
        }

        /// <summary>
        /// Gets the soft delete filter.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        private static LambdaExpression GetSoftDeleteFilter<TEntity>() where TEntity : class, IHasSoftDelete
        {
            Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted;
            return filter;
        }
    }

    #endregion
}
