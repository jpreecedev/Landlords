namespace Landlords.ViewModels
{
    using System;
    using Model.Entities;

    public class TenantContactViewModel
    {
        public TenantContactViewModel()
        {
        }

        public TenantContactViewModel(TenantContact contact)
        {
            if (contact == null)
            {
                return;
            }

            Id = contact.Id;
            Name = contact.Name;
            MainContactNumber = contact.MainContactNumber;
            SecondaryContactNumber = contact.SecondaryContactNumber;
            Relationship = contact.Relationship;
            Comments = contact.Comments;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string MainContactNumber { get; set; }

        public string SecondaryContactNumber { get; set; }

        public string Relationship { get; set; }

        public string Comments { get; set; }
    }
}