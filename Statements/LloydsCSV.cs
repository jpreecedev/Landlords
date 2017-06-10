namespace Landlords.Statements
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Core;
    using CsvHelper;
    using Interfaces;
    using Microsoft.AspNetCore.Http;
    using ViewModels;

    public class LloydsCSV : IStatementProcessor
    {
        public ICollection<TransactionViewModel> Process(ICollection<IFormFile> files, Guid accountId)
        {
            if (files == null) throw new ArgumentNullException(nameof(files));
            if (accountId.IsDefault()) throw new ArgumentNullException();

            var result = new List<TransactionViewModel>();
            foreach (var formFile in files)
            {
                using (var reader = new CsvReader(new StreamReader(formFile.OpenReadStream())))
                {
                    while (reader.Read())
                    {
                        var item = new TransactionViewModel()
                        {
                            AccountId = accountId,
                            Date = reader.GetField<DateTime>("Transaction Date"),
                            PaymentType = reader.GetField<string>("Transaction Type"),
                            Reference = reader.GetField<string>("Transaction Description"),
                            Balance = reader.GetField<decimal>("Balance")
                        };

                        if (reader.TryGetField("Debit Amount", out decimal debitAmount))
                        {
                            item.Out = debitAmount;
                        }
                        if (reader.TryGetField("Credit Amount", out decimal creditAmount))
                        {
                            item.In = creditAmount;
                        }

                        result.Add(item);
                    }
                }
            }

            return result;
        }
    }
}