namespace Model.DataTypes
{
    using System.Linq;

    public class MaintenanceSeverity
    {
        public const string Notice = "Notice";
        public const string Message = "Message";
        public const string Urgent = "Urgent";
        public const string Important = "Important";
        public const string Immediate = "Immediate";

        public static LLDataType[] GetDefaultMaintenanceSeverities()
        {
            return new[]
                {
                    Notice,
                    Message,
                    Urgent,
                    Important,
                    Immediate
                }
                .Select(c => new LLDataType(c))
                .ToArray();
        }
    }
}