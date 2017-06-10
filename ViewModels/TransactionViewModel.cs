namespace Landlords.ViewModels
{
    using System;
    using Model.Validation;
    using Model.Entities;

    public class TransactionViewModel
    {
        public TransactionViewModel()
        {
            
        }

        public TransactionViewModel(Transaction transaction)
        {
            if (transaction == null)
            {
                return;
            }

            AccountId = transaction.AccountId;
            Account = transaction.Account;
            Date = transaction.Date;
            PaymentType = transaction.PaymentType;
            Reference = transaction.Reference;
            DestinationAccountNumber = transaction.DestinationAccountNumber;
            DestinationSortCode = transaction.DestinationSortCode;
            In = transaction.In;
            Out = transaction.Out;
            Balance = transaction.Balance;
        }

        [ValidateGuid]
        public Guid AccountId { get; set; }

        public Account Account { get; set; }

        public DateTime Date { get; set; }

        public string PaymentType { get; set; }

        public string Reference { get; set; }

        public string DestinationAccountNumber { get; set; }

        public string DestinationSortCode { get; set; }

        public decimal In { get; set; }

        public decimal Out { get; set; }

        public decimal Balance { get; set; }
    }
}