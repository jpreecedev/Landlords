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
    using System.Text.RegularExpressions;

    public class SantanderCSV : IStatementProcessor
    {
        public ICollection<TransactionViewModel> Process(ICollection<IFormFile> files, Guid accountId)
        {
            if (files == null) throw new ArgumentNullException(nameof(files));
            if (accountId.IsDefault()) throw new ArgumentNullException();

            var result = new List<TransactionViewModel>();
            Regex rgx = new Regex("[^a-zA-Z0-9 -.]");

            foreach (var formFile in files)
            {
                using (var reader = new CsvReader(new StreamReader(formFile.OpenReadStream())))
                {
                    reader.Configuration.Delimiter = ";";

                    while (reader.Read())
                    {
                        if (reader.CurrentRecord[0] == "Arranged overdraft limit")
                        {
                            continue;
                        }

                        var item = new TransactionViewModel()
                        {
                            AccountId = accountId,
                            Date = reader.GetField<DateTime>("Date"),
                            PaymentType = reader.GetField<string>("Type"),
                            Reference = reader.GetField<string>("Merchant/Description"),
                            Balance = rgx.Replace(reader.GetField<string>("Balance"), "").ToDecimal()
                        };

                        var creditOrDebit = rgx.Replace(reader.GetField<string>("Debit/Credit"), "").ToDecimal();
                        if (creditOrDebit > 0)
                        {
                            item.In = creditOrDebit;
                        }
                        else
                        {
                            item.Out = creditOrDebit;
                        }

                        result.Add(item);
                    }
                }
            }

            return result;
        }
    }
}