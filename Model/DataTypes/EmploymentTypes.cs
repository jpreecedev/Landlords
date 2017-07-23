using System.Linq;

namespace Model.DataTypes
{
    public static class EmploymentTypes
    {
        public const string Employed = "Employed";
        public const string SelfEmployed = "SelfEmployed";
        public const string Retired = "Retired";
        public const string Homemaker = "Homemaker";
        public const string Unemployed = "Unemployed";

        public static LLDataType[] GetDefaultEmploymentTypes()
        {
            return new[]
                {
                    Employed,
                    SelfEmployed,
                    Retired,
                    Homemaker,
                    Unemployed
                }
                .Select(c => new LLDataType(c))
                .ToArray();
        }
    }
}