namespace Model
{
    public class EmailTemplate : BaseModel
    {
        public string Key { get; set; }

        public string Description { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}