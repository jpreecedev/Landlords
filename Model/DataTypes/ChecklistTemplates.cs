namespace Model.DataTypes
{
    using System.Linq;

    public static class ChecklistTemplates
    {
        public const string CommentsOnly = "CommentsOnly";
        public const string DateOfAction = "DateOfAction";
        public const string CommentsAndDateOfAction = "CommentsAndDateOfAction";
        public const string DocumentUpload = "DocumentUpload";

        public static LLDataType[] GetDefaultChecklistTemplates()
        {
            return new[]
                {
                    CommentsOnly,
                    DateOfAction,
                    CommentsAndDateOfAction,
                    DocumentUpload
                }
                .Select(c => new LLDataType(c))
                .ToArray();
        }
    }
}