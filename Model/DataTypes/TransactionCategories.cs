namespace Model.DataTypes
{
    public static class TransactionCategories
    {
        public const string MortgagePayment = "Mortgage payment";
        public const string RentalPayment = "Rental payment (income)";
        public const string GasSafetyInspection = "Gas safety inspection";
        public const string Misc = "Miscellaneous portfolio expense";
        public const string VAT = "VAT";
        public const string CorporationTax = "Corporation tax";
        public const string RenovationCost = "Renovation cost";

        public static string[] GetDefaultTransactionCategories()
        {
            return new[]
            {
                MortgagePayment,
                RentalPayment,
                GasSafetyInspection,
                Misc,
                VAT,
                CorporationTax,
                RenovationCost
            };
        }
    }
}