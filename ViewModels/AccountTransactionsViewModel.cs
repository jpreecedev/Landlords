namespace Landlords.ViewModels
{
    using System.Collections.Generic;
    using Model.DataTypes;

    public class AccountTransactionsViewModel
    {
        public AccountTransactionsViewModel()
        {
            
        }

        public AccountTransactionsViewModel(ICollection<TransactionViewModel> transactions)
        {
            Transactions = transactions;
        }

        public ICollection<TransactionViewModel> Transactions { get; set; }

        public string[] DefaultTransactionCategories { get; } = TransactionCategories.GetDefaultTransactionCategories();
    }
}