﻿namespace Landlords.Interfaces
{
    using System;
    using ViewModels;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IPropertyDataProvider
    {
        Task<PropertyDetailsViewModel> NewAsync(Guid portfolioId);
        Task<ICollection<PropertyDetailsViewModel>> GetListAsync(Guid portfolioId);
        Task<PropertyDetailsViewModel> GetDetailsAsync(Guid propertyId);
        Task UpdateAsync(Guid portfolioId, PropertyDetailsViewModel viewModel);
        Task<ICollection<PropertyBasicDetailsViewModel>> GetBasicDetailsAsync(Guid userId);
        Task<Guid> PromoteAsync(Guid portfolioId, ShortlistedPropertyViewModel viewModel);
    }
}
