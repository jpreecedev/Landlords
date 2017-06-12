﻿namespace Model.DataTypes
{
    public static class AccountProviders
    {
        public const string Santander = "Santander";
        public const string Halifax = "Halifax";
        public const string Nationwide = "Nationwide";
        public const string LloydsBank = "LloydsBank";

        public static string[] GetDefaultAccountProviders()
        {
            return new[]
            {
                Santander,
                Halifax,
                Nationwide,
                LloydsBank
            };
        }
    }
}