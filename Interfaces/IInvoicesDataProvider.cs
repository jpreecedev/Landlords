namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface IInvoicesDataProvider
    {
        Task<ICollection<InvoiceViewModel>> GetAsync(Guid portfolioId);
        Task<InvoiceViewModel> GetByIdAsync(Guid portfolioId, Guid invoiceId);
        Task AddAsync(Guid portfolioId, InvoiceViewModel invoice);
        Task UpdateAsync(Guid portfolioId, InvoiceViewModel invoice);
        Task DeleteAsync(Guid portfolioId, Guid invoiceId);
    }
}