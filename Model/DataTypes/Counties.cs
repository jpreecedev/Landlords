namespace Model.DataTypes
{
    using System.Linq;

    public static class Counties
    {
        public const string Cheshire = "Cheshire";
        public const string Lancashire = "Lancashire";

        public static LLDataType[] GetDefaultCounties()
        {
            return new[]
                {
                    Cheshire,
                    Lancashire
                }
                .Select(c => new LLDataType(c))
                .ToArray();
        }
    }
}