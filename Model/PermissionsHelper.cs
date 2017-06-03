namespace Model
{
    public static class DefaultPermissions
    {
        public static readonly string[] Administrator =
        {
            Permissions_PE.ListId,
            Permissions_PE.AddId,
            Permissions_PE.DeleteId,
            Permissions_PE.GetById,
            Permissions_PE.UsersId,
            Permissions_CL.ArchiveId,
            Permissions_CL.ArchivedId,
            Permissions_CL.CreateId,
            Permissions_CL.DeleteById,
            Permissions_CL.GetById,
            Permissions_CL.OverviewId,
            Permissions_CL.UpdateId,
            Permissions_CI.ToggleCompletedId,
            Permissions_CI.ToggleExpandedId,
            Permissions_CI.MoveId,
            Permissions_CI.ApplyTemplateId
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
            Permissions_PD.GetBasicId,
            Permissions_PI.DeleteId,
            Permissions_PI.UploadId,
            Permissions_CL.ArchiveId,
            Permissions_CL.ArchivedId,
            Permissions_CL.CreateId,
            Permissions_CL.DeleteById,
            Permissions_CL.GetById,
            Permissions_CL.OverviewId,
            Permissions_CL.UpdateId,
            Permissions_CI.ToggleCompletedId,
            Permissions_CI.ToggleExpandedId,
            Permissions_CI.MoveId,
            Permissions_CI.ApplyTemplateId
        };

        public static readonly string[] Accountant = { };

        public static readonly string[] OtherUser =
        {
            Permissions_P.ViewId,
            Permissions_PD.ViewId,
            Permissions_PD.GetListId,
            Permissions_PD.GetById,
            Permissions_CL.GetById,
            Permissions_CL.OverviewId,
        };

        public static readonly string[] AgencyAdministrator =
        {
            Permissions_LL.ListId,
            Permissions_CL.ArchiveId,
            Permissions_CL.ArchivedId,
            Permissions_CL.CreateId,
            Permissions_CL.DeleteById,
            Permissions_CL.GetById,
            Permissions_CL.OverviewId,
            Permissions_CL.UpdateId,
            Permissions_CI.ToggleCompletedId,
            Permissions_CI.ToggleExpandedId,
            Permissions_CI.MoveId,
            Permissions_CI.ApplyTemplateId
        };

        public static readonly string[] AgencyUser =
        {
            Permissions_LL.ListId,
            Permissions_CL.ArchiveId,
            Permissions_CL.ArchivedId,
            Permissions_CL.CreateId,
            Permissions_CL.DeleteById,
            Permissions_CL.GetById,
            Permissions_CL.OverviewId,
            Permissions_CL.UpdateId,
            Permissions_CI.ToggleCompletedId,
            Permissions_CI.ToggleExpandedId,
            Permissions_CI.MoveId,
            Permissions_CI.ApplyTemplateId
        };
    }
}