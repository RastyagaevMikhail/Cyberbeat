using System;
using System.Globalization;
using System.Linq;
using System.Text;

using UnityEngine;
namespace GameCore
{
    public static class StringExtensions
    {
        public static string ToColor (this string str, Color color)
        {
            string HEX_Color = ColorUtility.ToHtmlStringRGB (color);
            return $"<b><color=\"#{HEX_Color}\">{str}</color></b>";
        }
        public static string black (this string str) { return str.ToColor (Color.black); }
        public static string blue (this string str) { return str.ToColor (Color.blue); }
        public static string clear (this string str) { return str.ToColor (Color.clear); }
        public static string cyan (this string str) { return str.ToColor (Color.cyan); }
        public static string gray (this string str) { return str.ToColor (Color.gray); }
        public static string green (this string str) { return str.ToColor (Color.green); }
        public static string grey (this string str) { return str.ToColor (Color.grey); }
        public static string magenta (this string str) { return str.ToColor (Color.magenta); }
        public static string red (this string str) { return str.ToColor (Color.red); }
        public static string white (this string str) { return str.ToColor (Color.white); }
        public static string yellow (this string str) { return str.ToColor (Color.yellow); }
        /// <summary>
        /// Eg MY_INT_VALUE => MyIntValue
        /// </summary>
        public static string ToTitleCase (this string input)
        {
            var builder = new StringBuilder ();
            for (int i = 0; i < input.Length; i++)
            {
                var current = input[i];
                if (current == '_' && i + 1 < input.Length)
                {
                    var next = input[i + 1];
                    if (char.IsLower (next))
                    {
                        next = char.ToUpper (next, CultureInfo.InvariantCulture);
                    }

                    builder.Append (next);
                    i++;
                }
                else
                {
                    builder.Append (current);
                }
            }

            return builder.ToString ();
        }

        /// <summary>
        /// Returns whether or not the specified string is contained with this string
        /// </summary>
        public static bool Contains (this string source, string toCheck, StringComparison comparisonType)
        {
            return source.IndexOf (toCheck, comparisonType) >= 0;
        }

        /// <summary>
        /// Ex: "thisIsCamelCase" -> "This Is Camel Case"
        /// </summary>
        public static string SplitPascalCase (this string input)
        {
            if (input == null || input.Length == 0)
            {
                return input;
            }

            StringBuilder sb = new StringBuilder (input.Length);

            if (char.IsLetter (input[0]))
            {
                sb.Append (char.ToUpper (input[0]));
            }
            else
            {
                sb.Append (input[0]);
            }

            for (int i = 1; i < input.Length; i++)
            {
                char c = input[i];

                if (char.IsUpper (c) && !char.IsUpper (input[i - 1]))
                {
                    sb.Append (' ');
                }

                sb.Append (c);
            }

            return sb.ToString ();
        }

        /// <summary>
        /// Returns true if this string is null, empty, or contains only whitespace.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <returns><c>true</c> if this string is null, empty, or contains only whitespace; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrWhitespace (this string str)
        {
            if (!string.IsNullOrEmpty (str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (char.IsWhiteSpace (str[i]) == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public static string AsFormat (this string format, params object[] ps)
        {
            return string.Format (format, ps);
        }
        public static bool IsUpper (this char c)
        {
            return System.Char.IsUpper (c);
        }
        public static bool IsLower (this char c)
        {
            return System.Char.IsLower (c);
        }
        public static bool IsUpper (this string c)
        {
            return c.SingleOrDefault ().IsUpper ();
        }
        public static bool IsLower (this string c)
        {
            return c.SingleOrDefault ().IsLower ();
        }
        public static string ToCapitalize (this string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper (str[0]) + str.Substring (1);

            return str.ToUpper ();
        }
        public static bool IsNullOrEmpty (this string str)
        {
            return string.IsNullOrEmpty (str);
        }
        public static string ReplaceByRegex (this string str, string pattern, string replacemnt)
        {
            return System.Text.RegularExpressions.Regex.Replace (str, pattern, replacemnt);
        }
    }
}
