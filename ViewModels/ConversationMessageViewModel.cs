namespace Landlords.ViewModels
{
    using System;
    using Model.Validation;

    public class ConversationMessageViewModel
    {
        public Guid Id { get; set; }

        [RequiredGuid]
        public Guid ConversationId { get; set; }

        public string Message { get; set; }

        public DateTime? Seen { get; set; }

        [RequiredGuid]
        public Guid TenantId { get; set; }

        [RequiredGuid]
        public Guid LandlordId { get; set; }

        [MustMatchGuid("TenantId", "LandlordId"), RequiredGuid]
        public Guid SenderId { get; set; }
    }
}