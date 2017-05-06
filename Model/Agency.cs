namespace Model
{
    using Database;
    using System.Collections.Generic;

    public class Agency : BaseModel
    {
        public string Name { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}