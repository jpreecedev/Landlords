namespace Model
{
    public static class DefaultPermissions
    {
        public static readonly string[] Administrator =
        {
            Permissions_PE.UpdateId,
            Permissions_PE.ListId
        };

        public static readonly string[] Landlord =
        {
            Permissions_P.UpdateId,
            Permissions_P.ViewId,
            Permissions_PD.ViewId,
            Permissions_PD.NewId,
            Permissions_PD.GetListId,
            Permissions_PD.GetById,
            Permissions_PD.UpdateId,
            Permissions_PI.DeleteId,
            Permissions_PI.UploadId
        };

        public static readonly string[] Accountant = { };

        public static readonly string[] OtherUser =
        {
            Permissions_P.ViewId,
            Permissions_PD.ViewId,
            Permissions_PD.GetListId,
            Permissions_PD.GetById
        };

        public static readonly string[] AgencyAdministrator =
        {
            Permissions_LL.ListId
        };

        public static readonly string[] AgencyUser =
        {
            Permissions_LL.ListId
        };
    }
}