namespace Model.Entities
{
    public class Supplier : BaseModel
    {
        public string Name { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string AddressLine3 { get; set; }

        public string TownOrCity { get; set; }

        public string PostCode { get; set; }

        public string MainContactNumber { get; set; }

        public string SecondaryContactNumber { get; set; }

        public string EmailAddress { get; set; }
    }
}