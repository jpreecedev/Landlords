namespace Model.Entities
{
    using System;
    using Database;

    public class ConversationMessage : BaseModel
    {
        public Conversation Conversation { get; set; }

        public Guid ConversationId { get; set; }
        
        public string Message { get; set; }

        public DateTime? Seen { get; set; }

        public Guid SenderId { get; set; }

        public ApplicationUser Sender { get; set; }
    }
}