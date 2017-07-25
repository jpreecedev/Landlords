using System.Linq;

namespace Model.DataTypes
{
    public static class TenantContactTypes
    {
        public const string Referee = "Referee";
        public const string EmergencyContact = "Emergency Contact";
        public const string NextOfKin = "Next of Kin";
        public const string Family = "Family";

        public static LLDataType[] GetDefaultTenantContactTypes()
        {
            return new[]
                {
                    Referee,
                    EmergencyContact,
                    NextOfKin,
                    Family
                }
                .Select(c => new LLDataType(c))
                .ToArray();
        }
    }
}