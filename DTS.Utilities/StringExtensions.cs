using System;
using System.IO;
using System.Linq;

namespace DTS.Utilities
{
    public static class StringExtensions
    {
        public static string FormatEx(this string thisString, params object[] args)
        {
            return String.Format(thisString, args);
        }

        public static string PadTo(this string thisString, int length)
        {
            thisString = thisString ?? "";

            return thisString.Length < length
                    ? thisString.PadRight(length)
                    : thisString;
        }

        public static void CreateDirectories(this string thisString)
        {
            string[] parts = thisString.Split('\\');
            string directoryPath = parts[0] + "\\";

            parts = parts.Skip(1).Take(parts.Length - 2).ToArray();

            foreach (string part in parts)
            {
                directoryPath += "\\" + part;
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
            }
        }
    }
}
