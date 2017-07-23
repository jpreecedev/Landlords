namespace Model.DataTypes
{
    using System.Linq;

    public static class PropertyType
    {
        public const string Bungalow = "Bungalow";
        public const string Detached = "Detached";
        public const string Flat = "Flat";
        public const string House = "House";
        public const string SemiDetached = "Semi-detached";
        public const string Terrace = "Terrace";

        public static LLDataType[] GetDefaultPropertyTypes()
        {
            return new[]
                {
                    Bungalow,
                    Detached,
                    Flat,
                    House,
                    SemiDetached,
                    Terrace
                }
                .Select(c => new LLDataType(c))
                .ToArray();
        }
    }
}