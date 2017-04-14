namespace Landlords.Identity
{
    using System.Collections.Generic;
    using IdentityServer4.Test;

    public class Users
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "tempuser",
                    Password = "temppassword"
                }
            };
        }
    }
}