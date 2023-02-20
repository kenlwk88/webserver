using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Convert a string to camelCase
        /// </summary>
        /// <param name="value"></param>
        /// <returns>AnyAttributes -> anyAttributes , _localService -> localService</returns>
        public static string ToCamelCase(this string value)
        {
            var x = value.Replace("_", "");
            if (x.Length == 0) return "null";
            x = Regex.Replace(x, "([A-Z])([A-Z]+)($|[A-Z])", m => m.Groups[1].Value + m.Groups[2].Value.ToLower() + m.Groups[3].Value);
            return char.ToLower(x[0]) + x[1..];
        }

        /// <summary>
        /// Convert a string to PascalCase
        /// </summary>
        /// <param name="value"></param>
        /// <returns>anyAttributes -> AnyAttributes , _localService -> LocalService</returns>
        public static string ToPascalCase(this string value)
        {
            var x = ToCamelCase(value);
            return char.ToUpper(x[0]) + x[1..];
        }

        /// <summary>
        /// Try deserialize string to object class, else return original value
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static object TryParseJsonToObject(this string str)
        {
            try
            {
                return JsonConvert.DeserializeObject(str);
            }
            catch (Exception)
            {
                return str;
            }
        }

        public static T TryParseJsonToObject<T>(this string str)
        {
            if (str is null) return default;
            try
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            catch (Exception)
            {
                return default;
            }
        }

        /// <summary>
        /// Check the string has trailing whitespace
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool HasTrailingSpaces(this string str)
        {
            if (char.IsWhiteSpace(str[0]) || char.IsWhiteSpace(str[^1]))
                return true;
            return false;
        }

        /// <summary>
        /// Removes all leading and trailing white-space characters from the current string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimAll(this string str)
        {
            if (str != null) return str.Trim();
            return str;
        }

        /// <summary>
        /// Removes all whitespaces from the current string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveWhitespaces(this string str)
        {
            Regex regex = new(@"\s+");
            return regex.Replace(str, string.Empty);
        }

        /// <summary>
        /// Replace white-space characters in string with underscore.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceWhitespaceWithUnderscore(this string str)
        {
            str = str?.Replace(" ", "_");
            return str;
        }

        /// <summary>
        /// Replace underscore in string with whitespace.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceUnderscoreWithWhitespace(this string str)
        {
            str = str?.Replace("_", " ");
            return str;
        }

        /// <summary>
        /// Serialize object into string with camelCase property format
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string JsonSerializeWithCamelCaseProperty(this object obj)
        {
            return JsonConvert.SerializeObject(obj,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }

        /// <summary>
        /// Get string value between [first] a and [last] b.
        /// </summary>
        public static string Between(this string str, string a, string b)
        {
            int posA = str.IndexOf(a);
            int posB = str.LastIndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return str.Substring(adjustedPosA, posB - adjustedPosA);
        }

        /// <summary>
        /// Get string value after [first] a.
        /// </summary>
        public static string Before(this string str, string a)
        {
            int posA = str.IndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            return str.Substring(0, posA);
        }

        /// <summary>
        /// Get string value after [last] a.
        /// </summary>
        public static string After(this string str, string a)
        {
            int posA = str.LastIndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= str.Length)
            {
                return "";
            }
            return str.Substring(adjustedPosA);
        }

        /// <summary>
        /// Convert string to byte array
        /// </summary>
        public static byte[] ToByteArrayFromString(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        /// <summary>
        /// Convert base64 string to byte array
        /// </summary>
        public static byte[] ToByteArrayFromBase64String(this string str)
        {
            return Convert.FromBase64String(str);
        }

        /// <summary>
        /// Convert string to stream
        /// </summary>
        public static Stream ToStream(this string str)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(str);
            return new MemoryStream(byteArray);
        }

        /// <summary>
        /// Determines whether this instance and another specified System.String object have the same value.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool NotEquals(this string str, string obj)
        {
            return !str.Equals(obj);
        }

        /// <summary>
        /// Determines whether this string and a specified System.String object have the
        /// same value. A parameter specifies the culture, case, and sort rules used in the
        /// comparison.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="obj"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static bool NotEquals(this string str, string obj, StringComparison comparison)
        {
            return !str.Equals(obj, comparison);
        }

        /// <summary>
        /// Convert list to string
        /// </summary>
        public static string StringJoin(this List<string> obj)
        {
            return string.Join(", ", obj);
        }

        /// <summary>
        /// Convert list to string
        /// </summary>
        public static string StringJoin(this IEnumerable<int> obj)
        {
            return string.Join(", ", obj);
        }

        /// <summary>
        /// Convert IEnumerable to string
        /// </summary>
        public static string StringJoin(this IEnumerable<string> obj)
        {
            return string.Join(", ", obj);
        }
    }
}
