using System.Linq;

namespace Model.DataTypes
{
    public static class Titles
    {
        public const string Mr = "Mr";
        public const string Master = "Master";
        public const string Mrs = "Mrs";
        public const string Miss = "Miss";
        public const string Ms = "Ms";
        public const string Other = "Other";

        public static LLDataType[] GetDefaultTitles()
        {
            return new[]
                {
                    Mr,
                    Master,
                    Mrs,
                    Miss,
                    Ms,
                    Other
                }
                .Select(c => new LLDataType(c))
                .ToArray();
        }
    }
}