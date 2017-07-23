using System.Linq;

namespace Model.DataTypes
{
    public static class PaymentTerms
    {
        public const string Weekly = "Weekly";
        public const string TwoWeekly = "Two Weekly";
        public const string FourWeekly = "Four Weekly";
        public const string Monthly = "Monthly";
        public const string SixMonthly = "Six Monthly";
        public const string Annually = "Annually";

        public static LLDataType[] GetDefaultPaymentTerms()
        {
            return new[]
                {
                    Weekly,
                    TwoWeekly,
                    FourWeekly,
                    Monthly,
                    SixMonthly,
                    Annually
                }
                .Select(c => new LLDataType(c))
                .ToArray();
        }
    }
}