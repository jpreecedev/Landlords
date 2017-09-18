namespace Landlords.ViewModels
{
    using System;
    using System.Text;
    using Model.Database;

    public class ApplicationUserViewModel
    {
        public ApplicationUserViewModel()
        {
        }

        public ApplicationUserViewModel(ApplicationUser applicationUser)
        {
            if (applicationUser == null)
            {
                return;
            }

            Title = applicationUser.Title;
            FirstName = applicationUser.FirstName;
            MiddleName = applicationUser.MiddleName;
            LastName = applicationUser.LastName;
            SecondaryPhoneNumber = applicationUser.SecondaryPhoneNumber;
            AvailableFrom = applicationUser.AvailableFrom;
            AvailableTo = applicationUser.AvailableTo;
            AgencyId = applicationUser.AgencyId;
        }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string SecondaryPhoneNumber { get; set; }

        public DateTime? AvailableFrom { get; set; }

        public DateTime? AvailableTo { get; set; }

        public Guid? AgencyId { get; set; }

        public string Name
        {
            get
            {
                var builder = new StringBuilder();

                if (!string.IsNullOrEmpty(Title))
                {
                    builder.AppendFormat("{0} ", Title);
                }
                if (!string.IsNullOrEmpty(FirstName))
                {
                    builder.AppendFormat("{0} ", FirstName);
                }
                if (!string.IsNullOrEmpty(LastName))
                {
                    builder.AppendFormat("{0}", LastName);
                }

                return builder.ToString();
            }
        }
    }
}