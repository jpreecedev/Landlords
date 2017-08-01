namespace Landlords.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Model.Validation;

    public class ConversationViewModel
    {
        [RequiredGuid]
        public Guid ConversationId { get; set; }

        [RequiredGuid]
        public Guid TenantId { get; set; }

        [RequiredGuid]
        public Guid LandlordId { get; set; }

        public string TenantFirstName { get; set; }

        public string TenantLastName { get; set; }

        public string LandlordFirstName { get; set; }

        public string LandlordLastName { get; set; }

        public ICollection<ConversationMessageViewModel> Messages { get; set; }
    }
}