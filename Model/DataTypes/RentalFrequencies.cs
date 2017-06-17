namespace Model.DataTypes
{
    public static class RentalFrequencies
    {
        public const string Weekly = "Weekly";
        public const string TwoWeekly = "Two Weekly";
        public const string FourWeekly = "Four Weekly";
        public const string Monthly = "Monthly";
        public const string ThreeMonthly = "Three Monthly";
        public const string SixMonthly = "Six Monthly";
        public const string Annually = "Annually";

        public static string[] GetDefaultRentalFrequencies()
        {
            return new[]
            {
                Weekly,
                TwoWeekly,
                FourWeekly,
                Monthly,
                ThreeMonthly,
                SixMonthly,
                Annually
            };
        }
    }
}