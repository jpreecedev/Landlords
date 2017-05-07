namespace Model
{
    using System.Collections.Generic;
    using Permissions;

    public static class DefaultPermissions
    {
        public static readonly string[] Administrator =
        {
            Profile.Update,
            Profile.View
        };

        public static readonly string[] Landlord =
        {
            Profile.Update,
            Profile.View,
            Permissions.PropertyDetails.ViewData,
            Permissions.PropertyDetails.New,
            Permissions.PropertyDetails.GetList,
            Permissions.PropertyDetails.GetById,
            Permissions.PropertyDetails.Update,
            Permissions.PropertyImage.Delete,
            Permissions.PropertyImage.Upload
        };

        public static readonly string[] Accountant = { };

        public static readonly string[] OtherUser =
        {
            Profile.Update,
            Profile.View,
            Permissions.PropertyDetails.ViewData,
            Permissions.PropertyDetails.GetList,
            Permissions.PropertyDetails.GetById
        };

        public static readonly string[] AgencyAdministrator =
        {
            LandlordList.List
        };

        public static readonly string[] AgencyUser =
        {
            LandlordList.List
        };
    }
}