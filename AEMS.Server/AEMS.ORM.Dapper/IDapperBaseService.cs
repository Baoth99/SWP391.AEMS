using System;

namespace AEMS.ORM.Dapper
{
    internal interface IDapperBaseService : IDisposable
    {
        new void Dispose();
    }
}
