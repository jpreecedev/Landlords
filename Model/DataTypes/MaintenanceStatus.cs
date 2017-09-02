namespace Model.DataTypes
{
    using System.Linq;

    public class MaintenanceStatus
    {
        public const string Open = "Open";
        public const string Closed = "Closed";
        public const string InProgress = "In progress";
        public const string ActionRequired = "Action required";

        public static LLDataType[] GetDefaultMaintenanceStatuses()
        {
            return new[]
                {
                    Open,
                    Closed,
                    InProgress,
                    ActionRequired
                }
                .Select(c => new LLDataType(c))
                .ToArray();
        }
    }
}