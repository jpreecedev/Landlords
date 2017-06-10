namespace Landlords.ViewModels
{
    using Model.Entities;

    public class EmailViewModel
    {
        public EmailViewModel()
        {
        }

        public EmailViewModel(EmailTemplate template)
        {
            Template = template;
        }

        public string RecipientName { get; set; }
        public string RecipientEmail { get; set; }

        public EmailTemplate Template { get; set; }

        public void SetBody(params string[] parameters)
        {
            if (Template == null)
            {
                return;
            }

            Template.Body = string.Format(Template.Body, parameters);
        }
    }
}