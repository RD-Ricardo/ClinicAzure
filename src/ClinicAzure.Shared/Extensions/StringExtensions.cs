using System.Text;

namespace ClinicAzure.Shared.Extensions
{
    public static  class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            if (str.Length == 1)
                return str.ToLowerInvariant();
            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        public static string ToPascalCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            if (str.Length == 1)
                return str.ToUpperInvariant();
            return char.ToUpperInvariant(str[0]) + str.Substring(1);
        }

        public static string ToSnakeCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            var sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (char.IsUpper(c) && i > 0 && char.IsLower(str[i - 1]))
                {
                    sb.Append('_');
                }
                sb.Append(char.ToLowerInvariant(c));
            }
            return sb.ToString();
        }
    }
}
