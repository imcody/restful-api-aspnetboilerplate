using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ResponsibleSystem.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Checks if a string contains another string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="containsValue"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool Contains(this string source, string containsValue, StringComparison comp = StringComparison.OrdinalIgnoreCase)
        {
            return source.IndexOf(containsValue, comp) >= 0;
        }

        /// <summary>
        /// Converts a &quot;PascalCase&quot; string to &quot;Pascal Case&quot;,
        /// &quot;camelCase&quot; string to &quot;Camel Case&quot;, and replaces all underscores with spaces.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetFriendlyName(this string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var sb = new StringBuilder();
            char prev = default(char);

            // trim excess whitespace
            name = name.Trim();

            name = name.Replace("_", " ");

            for (int x = 0; x < name.Length; ++x)
            {
                char c = name[x];

                if (x == 0)
                {
                    // uppercase the first character to handle camelCase
                    c = char.ToUpper(c);
                }

                if (c == ' ' && prev == ' ')
                    continue;

                if (sb.Length > 0 && char.IsUpper(c) && prev != ' ')
                {
                    // encountered another capital character that isn't directly following a space, prefix it with a space
                    // this converts CamelCase to Camel Case
                    sb.Append(" " + c);
                }
                else
                {
                    // otherwise, just append it normally
                    sb.Append(c);
                }

                prev = c;
            }

            return sb.ToString().CapitalizeAcronyms();
        }

        /// <summary>
        /// Returns a new string with common acronyms capitalized. This will only capitalize the acronyms
        /// if they stand alone as their own words within sentences, not acronyms within words.
        /// 
        /// <para>
        ///     i.e.: "Get jpg" will be capitalized to "Get JPG", but "Getjpg" will remain "Getjpg".
        /// </para>
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A new string with capitalized acronyms</returns>
        public static string CapitalizeAcronyms(this string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var splits = name
                // handle individual words so that we don't replace substrings of words
                .Split(new[] { ' ', }, StringSplitOptions.RemoveEmptyEntries)
                .Select(t =>
                {
                    string[] acronyms =
                    {
                        "URL", "XML", "AJAX", "DNS", "HTML", "HTML5", "FTP", "HTTP", "HTTPS",
                        "PDF", "JPG", "PNG", "GIF", "SEO", "SVG", "URI"
                    };

                    foreach (var acro in acronyms)
                    {
                        if (t.Equals(acro, StringComparison.OrdinalIgnoreCase))
                        {
                            // if our word matches an acronym, uppercase it
                            return t.ToUpper();
                        }
                    }

                    return t;
                });

            return string.Join(" ", splits);
        }

        /// <summary>
        /// Converts a PascalCaseString to camelCase.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            if (!char.IsUpper(s[0]))
                return s;

            char[] chars = s.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                bool hasNext = (i + 1 < chars.Length);
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                    break;

                chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
            }

            return new string(chars);
        }

        public static string FirstLetterToUpperCultureInvariant(this string text)
        {
            return char.ToUpperInvariant(text[0]) +
                                    text.Substring(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string AddDelimiterAtCaps(this string s, string delimiter = " ")
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            return r.Replace(s, delimiter);
        }

        public static bool IsAlphaNumeric(this string s)
        {
            return Regex.IsMatch(s, "^[a-zA-Z0-9]+$");
        }

        public static bool IsAlphanumericOrUnderscore(this string s)
        {
            return Regex.IsMatch(s, "^[a-zA-Z0-9_]*$");
        }

        public static string GetLast(this string source, int length)
        {
            if (length >= source.Length)
            {
                return source;
            }
            return source.Substring(source.Length - length);
        }

        public static string GenerateUnique(int length)
        {
            var random = new Random();
            return
                new string(
                    Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length)
                        .Select(c => c[random.Next(c.Length)])
                        .ToArray());
        }

        public static string AddStartAndEndSlashes(this string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return url;

            if (url.ShouldHaveStartSlash())
                url = "/" + url;

            if (url.ShouldHaveEndSlash())
                url = url + "/";

            return url;
        }

        public static bool ShouldHaveStartSlash(this string url)
        {
            return !url.StartsWith("/");
        }

        public static bool ShouldHaveEndSlash(this string url)
        {
            if (url.EndsWith("/"))
                return false;

            var uri = new Uri(url.StartsWith("http") ? url : "http://anyurl.com/" + url);
            if (!string.IsNullOrWhiteSpace(uri.Query) || !string.IsNullOrWhiteSpace(uri.Fragment))
                return false;

            if (Path.HasExtension(url))
                return false;

            return true;
        }

        public static bool ArePathsEqual(this string path, string inputPath)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
                return false;

            return string.Equals(path.AddStartAndEndSlashes(), inputPath.AddStartAndEndSlashes(),
                StringComparison.InvariantCultureIgnoreCase);
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static bool IsProtectedParamName(this string fieldName)
        {
            string[] protectedFields = { "password", "pwd", "pass", "creditcard", "credit_card", "ccnumber", "cid", "card_number", "cardnumber" };
            return protectedFields.Contains(fieldName.ToLower());
        }

        public static T GetValueOrDefault<T>(this string input)
        {
            try
            {
                return (T)Convert.ChangeType(input, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Returns whether or not the string is fully uppercase.
        /// Ignores non-letter characters for the purpose of determining uppercase-ness.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsUpper(this string input)
        {
            return input.All(c => char.IsUpper(c) || !char.IsLetter(c));
        }

        /// <summary>
        /// Returns whether or not the string is fully lowercase.
        /// Ignores non-letter characters for the purpose of determining lowercase-ness.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsLower(this string input)
        {
            return input.All(c => char.IsLower(c) || !char.IsLetter(c));
        }

        /// <summary>
        /// Returns no more that maxLength chars
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Truncate(this string str, int maxLength)
        {
            return str.Substring(0, Math.Min(str.Length, maxLength));
        }


        public static T TryDeserializeJson<T>(this string jsonInput)
        {
            T result = default(T);
            try
            {
                result = JsonConvert.DeserializeObject<T>(jsonInput);
            }
            catch (Exception) // update to better error checking
            { }

            return result;
        }
    }
}
