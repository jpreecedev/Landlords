namespace Landlords.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Model.Entities;
    using Model.Validation;

    public class TenantAddressViewModel
    {
        public TenantAddressViewModel()
        {
        }

        public TenantAddressViewModel(bool isChild, TenantAddress tenantAddress)
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
            IsChild = isChild;
        }

        public Guid Id { get; set; }

        public bool IsChild { get; private set; }
        
        [Required, MinLength(2)]
        public string Street { get; set; }

        [Display(Name = "Town or city")]
        [Required, MinLength(2)]
        public string TownOrCity { get; set; }

        public string CountyOrRegion { get; set; }
        
        [Required, MinLength(5), MaxLength(7)]
        public string Postcode { get; set; }

        [Required]
        public string Country { get; set; }

        [Display(Name = "Years at address")]
        [RangeIfTrue("IsChild", 1, 100)]
        public int YearsAtAddress { get; set; }

        [Display(Name = "Months at address")]
        [RangeIfTrue("IsChild", 0, 12)]
        public int MonthsAtAddress { get; set; }

        public bool IsDeleted { get; set; }
    }
}