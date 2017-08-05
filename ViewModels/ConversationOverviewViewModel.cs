namespace Landlords.ViewModels
{
    using System.Collections.Generic;

    public class ConversationOverviewViewModel
    {
        public ICollection<ContactViewModel> Contacts { get; set; }

        public ICollection<ConversationViewModel> Conversations { get; set; }
    }
}