namespace Model.DataTypes
{
    using System.Linq;

    public static class TenancyTypes
    {
        public const string Agricultural = "Agricultural";
        public const string Assured = "Assured";
        public const string AST = "AST";
        public const string CommonLaw = "Common Law";
        public const string License = "License";
        public const string Regulated = "Regulated";

        public static LLDataType[] GetDefaultTenancyTypes()
        {
            return new[]
                {
                    Agricultural,
                    Assured,
                    AST,
                    CommonLaw,
                    License,
                    Regulated
                }
                .Select(c => new LLDataType(c))
                .ToArray();
        }
    }
}