namespace Model.DataTypes
{
    using System.Linq;

    public static class AccountTypes
    {
        public const string Bank = "Bank Account";
        public const string CreditCard = "Credit Card";
        public const string DebitCard = "Debit Card";

        public static LLDataType[] GetDefaultAccountTypes()
        {
            return new[]
                {
                    Bank,
                    CreditCard,
                    DebitCard
                }
                .Select(c => new LLDataType(c))
                .ToArray();
        }
    }
}