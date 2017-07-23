using System.Linq;

namespace Model.DataTypes
{
    public static class TenantContactTypes
    {
        public const string Referee = "Referee";
        public const string EmergencyContact = "EmergencyContact";
        public const string NextOfKin = "NextOfKin";
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