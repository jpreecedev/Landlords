namespace Model.DataTypes
{
    public static class MortgageProviders
    {
        public const string Halifax = "Halifax";

        public static string[] GetDefaultMortgageProviders()
        {
            return new[]
            {
                Halifax
            };
        }
    }
}