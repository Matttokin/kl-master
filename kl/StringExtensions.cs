using System;

namespace DataReaderToJsonExample.JsonSerializer
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string value)
        {
            return Char.ToLowerInvariant(value[0]) + value.Substring(1);
        }
    }
}
