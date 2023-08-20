using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace mcr.Data.Helpers
{
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter() : base(
        dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue), 
        dateTime => DateOnly.FromDateTime(dateTime))
    { }
}
}