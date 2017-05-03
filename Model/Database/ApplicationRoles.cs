namespace Model.Database
{
    public static class ApplicationRoles
    {
        public const string SiteAdministrator = "Administrator";
        public const string Landlord = "Landlord";
        public const string Accountant = "Accountant";
        public const string OtherUser = "OtherUser";
        public const string AgencyAdministrator = "Agency";
        public const string AgencyUser = "AgencyUser";

        public static string[] AllRoles = new[]
        {
            SiteAdministrator,
            Landlord,
            Accountant,
            OtherUser,
            AgencyAdministrator,
            AgencyUser
        };
    }
}