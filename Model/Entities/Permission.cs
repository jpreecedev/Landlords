namespace Model.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Permission : IPermission
    {
        public Permission()
        {
        }

        public Permission(string description)
        {
            Description = Description;
        }

        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        
        public string RouteId { get; set; }
        public string Description { get; set; }
    }
}