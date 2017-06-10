namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using ViewModels;

    public interface IStatementProcessor
    {
        ICollection<TransactionViewModel> Process(ICollection<IFormFile> files, Guid accountId);
    }
}