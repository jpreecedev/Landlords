namespace Model
{
    public class Permission : IPermission
    {
        public Permission()
        {
        }

        public Permission(string description, string displayText)
        {
            Description = description;
            DisplayText = displayText;
        }

        public string Description { get; set; }
        public string DisplayText { get; set; }
    }
}