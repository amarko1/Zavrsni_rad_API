using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UtcValueConverterSelector : ValueConverterSelector
    {
        private static readonly ValueConverterInfo DateTimeInfo
            = new ValueConverterInfo(typeof(DateTime), typeof(DateTime),
                i => new ValueConverter<DateTime, DateTime>(
                    v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
                    null));

        private static readonly ValueConverterInfo NullableDateTimeInfo
            = new ValueConverterInfo(typeof(DateTime?), typeof(DateTime?),
                i => new ValueConverter<DateTime?, DateTime?>(
                    v => v.HasValue ? (v.Value.Kind == DateTimeKind.Utc ? v.Value : v.Value.ToUniversalTime()) : v,
                    v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v,
                    null));

        public UtcValueConverterSelector(ValueConverterSelectorDependencies dependencies)
            : base(dependencies) { }

        public override IEnumerable<ValueConverterInfo> Select(Type modelClrType, Type providerClrType = null)
        {
            foreach (var converter in base.Select(modelClrType, providerClrType))
                yield return converter;

            if (modelClrType == typeof(DateTime))
                yield return DateTimeInfo;

            if (modelClrType == typeof(DateTime?))
                yield return NullableDateTimeInfo;
        }
    }
}
