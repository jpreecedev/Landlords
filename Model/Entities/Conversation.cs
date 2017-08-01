namespace Model.Entities
{
    using System;
    using System.Collections.Generic;
    using Database;

    public class Conversation : BaseModel
    {
        public Guid TenantId { get; set; }

        public ApplicationUser Tenant { get; set; }

        public Guid LandlordId { get; set; }

        public ApplicationUser Landlord { get; set; }

        public ICollection<ConversationMessage> Messages { get; set; }
    }
}