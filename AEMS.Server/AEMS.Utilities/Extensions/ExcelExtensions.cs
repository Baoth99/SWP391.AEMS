using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AEMS.Utilities
{
    public static class ExcelExtensions
    {
        public static ExcelRangeBase LoadFromCollectionFiltered<T>(this ExcelRangeBase baseRange, IEnumerable<T> collection, bool printHeader, bool hideCustom) where T : class
        {
            MemberInfo[] membersToInclude = typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !Attribute.IsDefined(p, typeof(ExcelIgnore)) && (!hideCustom || !Attribute.IsDefined(p, typeof(ExcelHidden))))
                .ToArray();

            return baseRange.LoadFromCollection(collection, printHeader, OfficeOpenXml.Table.TableStyles.None, BindingFlags.Instance | BindingFlags.Public, membersToInclude);
        }

        public static ExcelWorksheet WraptextCells<T>(this ExcelWorksheet worksheet, bool hideCustom) where T : class
        {
            MemberInfo[] membersToInclude = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                    .Where(p => !Attribute.IsDefined(p, typeof(ExcelIgnore)) && (!hideCustom || !Attribute.IsDefined(p, typeof(ExcelHidden))))
                                                    .ToArray();

            if (membersToInclude.Any())
            {
                foreach (var item in membersToInclude.Select((x, i) => new { x, i }))
                {
                    if (Attribute.IsDefined(item.x, typeof(CellWrapText)))
                    {
                        worksheet.Column(item.i + 1).Width = 50;
                        worksheet.Column(item.i + 1).Style.WrapText = true;
                    }
                }
            }
            return worksheet;
        }
    }

    public class ExcelIgnore : Attribute
    {
    }

    public class ExcelHidden : Attribute
    {
    }

    public class CellWrapText : Attribute
    {
    }
}
