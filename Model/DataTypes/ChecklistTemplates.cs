namespace Model.DataTypes
{
    public static class ChecklistTemplates
    {
        public const string CommentsOnly = "CommentsOnly";
        public const string DateOfAction = "DateOfAction";
        public const string CommentsAndDateOfAction = "CommentsAndDateOfAction";
        public const string DocumentUpload = "DocumentUpload";

        public static string[] GetDefaultChecklistTemplates()
        {
            return new[]
            {
                CommentsOnly,
                DateOfAction,
                CommentsAndDateOfAction,
                DocumentUpload
            };
        }
    }
}