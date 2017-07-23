namespace Model
{
    using System.Text.RegularExpressions;

    public static class Extensions
    {
        private static readonly Regex _regex = new Regex(@"(?<=[A-Z])(?=[A-Z][a-z]) | (?<=[^A-Z])(?=[A-Z]) | (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

        public static string SplitByCapitals(this string input)
        {
            return _regex.Replace(input, " ");
        }
    }
}