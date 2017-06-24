namespace Landlords.ViewModels
{
    using System;
    using Model.DataTypes;
    using Model.Entities;

    public class TenantAddressViewModel
    {
        public TenantAddressViewModel()
        {
        }

        public TenantAddressViewModel(TenantAddress tenantAddress)
        {
            if (tenantAddress == null)
            {
                return;
            }

            Id = tenantAddress.Id;
            Street = tenantAddress.Street;
            TownOrCity = tenantAddress.TownOrCity;
            CountyOrRegion = tenantAddress.CountyOrRegion;
            Postcode = tenantAddress.Postcode;
            Country = tenantAddress.Country;
        }

        public Guid Id { get; set; }

        public string Street { get; set; }

        public string TownOrCity { get; set; }

        public string CountyOrRegion { get; set; }

        public string Postcode { get; set; }

        public string Country { get; set; }
    }
}