namespace Model.DataTypes
{
    public static class TenantContactType
    {
        public const string Referee = "Referee";
        public const string EmergencyContact = "EmergencyContact";
        public const string NextOfKin = "NextOfKin";
        public const string Family = "Family";

        public static string[] GetDefaultTenantContactTypes()
        {
            return new[]
            {
                Referee,
                EmergencyContact,
                NextOfKin,
                Family
            };
        }
    }
}