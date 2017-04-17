namespace Model.DataTypes
{
    public static class Countries
    {
        public const string UnitedKingdom = "United Kingdom";

        public static string[] GetDefaultCountries()
        {
            return new[]
            {
                UnitedKingdom
            };
        }
    }
}