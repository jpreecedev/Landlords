namespace Model.DataTypes
{
    public static class Counties
    {
        public const string Cheshire = "Cheshire";
        public const string Lancashire = "Lancashire";

        public static string[] GetDefaultCounties()
        {
            return new[]
            {
                Cheshire,
                Lancashire
            };
        }
    }
}