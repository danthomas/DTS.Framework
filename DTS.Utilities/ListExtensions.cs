using System;
using System.Collections.Generic;
using System.Linq;

namespace DTS.Utilities
{
    public static class ListExtensions
    {

        public static string Layout(this List<List<object>> thisListList)
        {
            int[] widths = new int[thisListList.Max(item => item.Count)];

            foreach (List<object> list in thisListList)
            {
                for (int i = 0; i < widths.Length; ++i)
                {
                    widths[i] = Math.Max(widths[i], list[i] == null ? 0 : list[i].ToString().Length);
                }
            }

            string ret = "";

            foreach (List<object> list in thisListList)
            {
                int i = 0;

                ret += Environment.NewLine;

                ret = list.Aggregate(ret, (current, @object) => current + (@object == null ? "" : @object.ToString()).PadTo(widths[i++] + 1));
            }

            return ret;
        }
    }
}
