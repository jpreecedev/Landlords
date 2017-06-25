namespace Model.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Validation;

    public class TenantAddress : BaseModel
    {
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

        public Tenant Tenant { get; set; }

        [RequiredGuid]
        public Guid TenantId { get; set; }

        [Display(Name = "Years at address")]
        [Range(1, 100)]
        public int YearsAtAddress { get; set; }

        [Display(Name = "Months at address")]
        [Range(0, 12)]
        public int MonthsAtAddress { get; set; }
    }
}