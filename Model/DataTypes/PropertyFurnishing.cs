namespace Model.DataTypes
{
    public static class PropertyFurnishing
    {
        public const string Fully = "Fully";
        public const string Part = "Part";
        public const string None = "None";
        
        public static string[] GetDefaultFurnishings()
        {
            return new[]
            {
                Fully,
                Part,
                None
            };
        }
    }
}