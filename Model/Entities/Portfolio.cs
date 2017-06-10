namespace Model.Entities
{
    using System.Collections.Generic;

    public class Portfolio : BaseModel
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public ICollection<ApplicationUserPortfolio> Users { get; set; }
    }
}