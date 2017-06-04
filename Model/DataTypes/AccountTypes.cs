namespace Model.DataTypes
{
    public static class AccountTypes
    {
        public const string Bank = "Bank Account";
        public const string CreditCard = "Credit Card";
        public const string DebitCard = "Debit Card";

        public static string[] GetDefaultAccountTypes()
        {
            return new[]
            {
                Bank,
                CreditCard,
                DebitCard
            };
        }
    }
}