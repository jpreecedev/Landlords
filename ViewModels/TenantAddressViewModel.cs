namespace Landlords.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
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
            YearsAtAddress = tenantAddress.YearsAtAddress;
            MonthsAtAddress = tenantAddress.MonthsAtAddress;
        }

        public Guid Id { get; set; }
        
        [Required, MinLength(2)]
        public string Street { get; set; }

        [Display(Name = "Town or city")]
        [Required, MinLength(2)]
        public string TownOrCity { get; set; }

        public string CountyOrRegion { get; set; }
        
        [Required, MinLength(2)]
        public string Postcode { get; set; }

        [Required]
        public string Country { get; set; }

        [Display(Name = "Years at address")]
        [Range(1, 100)]
        public int YearsAtAddress { get; set; }

        [Display(Name = "Months at address")]
        [Range(0, 12)]
        public int MonthsAtAddress { get; set; }
    }
}