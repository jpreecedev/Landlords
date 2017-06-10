namespace Landlords.Permissions
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    public class BasePermissionsAttribute : TypeFilterAttribute
    {
        public BasePermissionsAttribute(Type type) : base(type)
        {
        }

        protected static Guid? TryParse(string input)
        {
            if (Guid.TryParse(input, out Guid result))
            {
                return result;
            }
            return null;
        }
    }
}