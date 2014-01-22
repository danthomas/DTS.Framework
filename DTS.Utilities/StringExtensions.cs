using System;

namespace DTS.Utilities
{
    public static class StringExtensions
    {
        public static string FormatEx(this string thisString, params object[] args)
        {
            return String.Format(thisString, args);
        }
    }
}
