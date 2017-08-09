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
        public Guid SenderId { get; set; }

        [RequiredGuid]
        public Guid ReceiverId { get; set; }

        public string SenderFirstName { get; set; }

        public string SenderLastName { get; set; }

        public string ReceiverFirstName { get; set; }

        public string ReceiverLastName { get; set; }

        public bool IsToReceiver { get; set; }

        public ICollection<ConversationMessageViewModel> Messages { get; set; }

        public ConversationViewModel ShallowClone()
        {
            return (ConversationViewModel) MemberwiseClone();
        }
    }
}