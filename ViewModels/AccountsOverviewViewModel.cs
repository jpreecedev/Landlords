namespace Landlords.ViewModels
{
    using System.Collections.Generic;

    public class AccountsOverviewViewModel
    {
        public AccountsOverviewViewModel()
        {
        }

        public AccountsOverviewViewModel(ICollection<AccountViewModel> accounts)
        {
            Accounts = accounts;
        }

        public ICollection<AccountViewModel> Accounts { get; set; }
    }
}