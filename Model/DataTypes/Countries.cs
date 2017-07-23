namespace Model.DataTypes
{
    using System.Linq;

    public static class Countries
    {
        public const string UnitedKingdom = "United Kingdom";

        public static LLDataType[] GetDefaultCountries()
        {
            return new[]
                {
                    UnitedKingdom
                }
                .Select(c => new LLDataType(c))
                .ToArray();
        }
    }
}