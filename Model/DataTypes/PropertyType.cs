namespace Model.DataTypes
{
    public static class PropertyType
    {
        public const string Bungalow = "Bungalow";
        public const string Detached = "Detached";
        public const string Flat = "Flat";
        public const string House = "House";
        public const string SemiDetached = "Semi-detached";
        public const string Terrace = "Terrace";
        
        public static string[] GetDefaultPropertyTypes()
        {
            return new[]
            {
                Bungalow,
                Detached,
                Flat,
                House,
                SemiDetached,
                Terrace
            };
        }
    }
}