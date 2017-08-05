namespace Landlords.ViewModels
{
    using System;
    using Model.Validation;
    using Newtonsoft.Json;

    public class ConversationMessageViewModel
    {
        public Guid Id { get; set; }

        [RequiredGuid]
        public Guid ConversationId { get; set; }

        public string Message { get; set; }

        public DateTime? Seen { get; set; }

        [RequiredGuid]
        public Guid SenderId { get; set; }

        [JsonIgnore]
        public Guid ReceiverId { get; set; }

        public DateTime Sent { get; set; }
    }
}