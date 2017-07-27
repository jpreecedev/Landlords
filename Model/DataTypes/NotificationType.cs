namespace Model.DataTypes
{
    using System.Linq;

    public class NotificationType
    {
        public const string Notice = "Notice";
        public const string Important = "Important";
        public const string Immediate = "Immediate";
        public const string Overdue = "Overdue";

        public static LLDataType[] GetDefaultNotificationTypes()
        {
            return new[]
                {
                    Notice,
                    Important,
                    Immediate,
                    Overdue
                }
                .Select(c => new LLDataType(c))
                .ToArray();
        }
    }
}