using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace KYExpress.Authentication
{
    public class TokenAuthOptions
    {
        public SymmetricSecurityKey SymmetricSecurityKey { get { return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey)); } }

        public string SecurityKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public SigningCredentials SigningCredentials { get { return new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256); } } 

        public TimeSpan Expiration { get; set; }
    }
}
