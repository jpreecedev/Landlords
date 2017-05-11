namespace Model
{
    using Permissions;

    public static class DefaultPermissions
    {
        public static readonly string[] Administrator =
        {
            Profile.UpdateId,
            Profile.ViewId
        };

        public static readonly string[] Landlord =
        {
            Profile.UpdateId,
            Profile.ViewId,
            Permissions.PropertyDetails.ViewId,
            Permissions.PropertyDetails.NewId,
            Permissions.PropertyDetails.GetListId,
            Permissions.PropertyDetails.GetById,
            Permissions.PropertyDetails.UpdateId,
            Permissions.PropertyImage.DeleteId,
            Permissions.PropertyImage.UploadId
        };

        public static readonly string[] Accountant = { };

        public static readonly string[] OtherUser =
        {
            Profile.ViewId,
            Permissions.PropertyDetails.ViewId,
            Permissions.PropertyDetails.GetListId,
            Permissions.PropertyDetails.GetById
        };

        public static readonly string[] AgencyAdministrator =
        {
            LandlordList.ListId
        };

        public static readonly string[] AgencyUser =
        {
            LandlordList.ListId
        };
    }
}