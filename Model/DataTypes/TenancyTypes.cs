namespace Model.DataTypes
{
    public static class TenancyTypes
    {
        public const string Agricultural = "Agricultural";
        public const string Assured = "Assured";
        public const string AST = "AST";
        public const string CommonLaw = "CommonLaw";
        public const string License = "License";
        public const string Regulated = "Regulated";

        public static string[] GetDefaultTenancyTypes()
        {
            return new[]
            {
                Agricultural,
                Assured,
                AST,
                CommonLaw,
                License,
                Regulated
            };
        }
    }
}