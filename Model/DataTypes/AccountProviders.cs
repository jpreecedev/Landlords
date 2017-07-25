namespace Model.DataTypes
{
    using System.Linq;

    public static class AccountProviders
    {
        public const string Santander = "Santander";
        public const string Halifax = "Halifax";
        public const string Nationwide = "Nationwide";
        public const string LloydsBank = "Lloyds Bank";

        public static LLDataType[] GetDefaultAccountProviders()
        {
            return new[]
                {
                    Santander,
                    Halifax,
                    Nationwide,
                    LloydsBank
                }
                .Select(c => new LLDataType(c))
                .ToArray();
            ;
        }
    }
}