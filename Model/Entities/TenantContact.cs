namespace Model.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Validation;

    public class TenantContact : BaseModel
    {
        [Required, MinLength(2)]
        public string Name { get; set; }

        [Display(Name = "Main contact number")]
        [Required, MinLength(2)]
        public string MainContactNumber { get; set; }

        public string SecondaryContactNumber { get; set; }

        [Required, MinLength(2)]
        public string Relationship { get; set; }

        public string Comments { get; set; }

        [RequiredGuid]
        public Guid TenantId { get; set; }

        public Tenant Tenant { get; set; }
    }
}