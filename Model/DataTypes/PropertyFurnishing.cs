using System.Linq;

namespace Model.DataTypes
{
    public static class PropertyFurnishing
    {
        public const string Fully = "Fully";
        public const string Part = "Part";
        public const string None = "None";

        public static LLDataType[] GetDefaultFurnishings()
        {
            return new[]
                {
                    Fully,
                    Part,
                    None
                }
                .Select(c => new LLDataType(c))
                .ToArray();
        }
    }
}