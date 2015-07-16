using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Notepad.Classes
{
    public static class Helper
    {
        public static IEnumerable<int> GetIndexes(string pText, string pSearchText, bool pCaseSensitive)
        {
            var indexes = new List<int>();
            var eStringComparison = pCaseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;
            var startIndex = 0;

            while (true)
            {
                var index = pText.IndexOf(pSearchText, startIndex, eStringComparison);
                if (index == -1) break;
                indexes.Add(index);
                startIndex = index + pSearchText.Length;
            }

            return indexes;
        }

        public static string FormatUsingObject(this string @this, object poObject)
        {
            return TypeDescriptor.GetProperties(poObject).Cast<PropertyDescriptor>().Aggregate(@this,
                    (current, oProperty) => current.Replace("{" + oProperty.Name + "}", oProperty.GetValue(poObject).ToStringOrNull()));
        }

        private static string ToStringOrNull(this object @this)
        {
            return @this?.ToString();
        }

        public static bool IsEmpty(this string @this)
        {
            return string.IsNullOrWhiteSpace(@this);
        }
    }
}