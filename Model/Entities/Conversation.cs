namespace Model.Entities
{
    using System;
    using System.Collections.Generic;
    using Database;

    public class Conversation : BaseModel
    {
        public Guid SenderId { get; set; }

        public ApplicationUser Sender { get; set; }

        public Guid ReceiverId { get; set; }

        public ApplicationUser Receiver { get; set; }

        public ICollection<ConversationMessage> Messages { get; set; }
    }
}