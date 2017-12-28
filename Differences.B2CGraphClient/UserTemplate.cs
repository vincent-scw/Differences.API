using System;
using System.Collections.Generic;
using System.Text;

namespace Differences.B2CGraphClient
{
    public class UserTemplate
    {
        public bool? AccountEnabled { get; set; }
        public List<SignInName> SignInNames { get; set; }
        public string CreationType { get; set; }
        public string DisplayName { get; set; }
        public string MailNickname { get; set; }
        public PasswordProfile PasswordProfile { get; set; }
        public string PasswordPolicies { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string FacsimileTelephoneNumber { get; set; }
        public string GivenName { get; set; }
        public string Mail { get; set; }
        public string Mobile { get; set; }
        public List<string> OtherMails { get; set; }
        public string PostalCode { get; set; }
        public string PreferredLanguage { get; set; }
        public string State { get; set; }
        public string StreetAddress { get; set; }
        public string Surname { get; set; }
        public string TelephoneNumber { get; set; }

        public static UserTemplate CreateDefault()
        {
            return new UserTemplate
            {
                AccountEnabled = true,
                CreationType = "LocalAccount",
                PasswordPolicies = "DisablePasswordExpiration"
            };
        }
    }

    public class SignInName
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public class PasswordProfile
    {
        public string Password { get; set; }
        public bool ForceChangePasswordNextLogin { get; set; }
    }
}
