namespace Model
{
    public static class Permissions
    {
        public const string ProfileUpdate = "Update profile";
        public const string ProfileView = "View profile";

        public const string PropertyDetailsViewData = "Get property details view data";
        public const string PropertyDetailsNew = "Create new property";
        public const string PropertyDetailsGetList = "Get list of properties";
        public const string PropertyDetailsGetById = "Get details for single property";
        public const string PropertyDetailsUpdate = "Update single property";

        public const string PropertyImageDelete = "Delete property image";
        public const string PropertyImageUpload = "Upload property image";

        public const string LandlordList = "View list of landlords";

        public static readonly string[] DefaultAdministratorPermissions =
        {
            ProfileUpdate,
            ProfileView
        };

        public static readonly string[] DefaultLandlordPermissions =
        {
            ProfileUpdate,
            ProfileView,
            PropertyDetailsViewData,
            PropertyDetailsNew,
            PropertyDetailsGetList,
            PropertyDetailsGetById,
            PropertyDetailsUpdate,
            PropertyImageDelete,
            PropertyImageUpload
        };

        public static readonly string[] DefaultAccountantPermissions = { };

        public static readonly string[] DefaultOtherUserPermissions =
        {
            ProfileUpdate,
            ProfileView,
            PropertyDetailsViewData,
            PropertyDetailsGetList,
            PropertyDetailsGetById,
        };

        public static readonly string[] DefaultAgencyAdministratorPermissions =
        {
            LandlordList
        };

        public static readonly string[] DefaultAgencyUserPermissions =
        {
            LandlordList
        };
    }
}
