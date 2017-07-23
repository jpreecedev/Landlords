namespace Model.DataTypes
{
    using System.Linq;

    public static class MortgageProviders
    {
        public const string Halifax = "Halifax";

        public static LLDataType[] GetDefaultMortgageProviders()
        {
            return new[]
                {
                    Halifax
                }
                .Select(c => new LLDataType(c))
                .ToArray();
        }
    }
}