using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MobaSpace.Core.Utils
{
    public static class StringUtils
    {
        public static string Capitalise(this string text, string targets, CultureInfo culture)
        {
            bool capitalise = true;
            var result = new StringBuilder(text.Length);

            foreach (char c in text)
            {
                if (capitalise)
                {
                    result.Append(char.ToUpper(c, culture));
                    capitalise = false;
                }
                else
                {
                    if (targets.Contains(c))
                        capitalise = true;

                    result.Append(c);
                }
            }

            return result.ToString();
        }
    }
}
