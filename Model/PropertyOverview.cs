using System;

namespace Model
{
    public class PropertyOverview
    {
        public int Id { get; set; }

        public Furnishing Furnishing { get; set; }

        public PropertyType Type { get; set; }

        public DateTime? ConstructionDate { get; set; }

        public decimal TargetRent { get; set; }
    }
}