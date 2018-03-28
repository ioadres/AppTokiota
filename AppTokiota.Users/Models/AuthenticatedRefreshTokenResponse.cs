using System;
using System.Runtime.Serialization;

namespace AppTokiota.Users.Models
{
    [DataContract]
    public class AuthenticatedRefreshTokenResponse
    {
        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }
       
        [DataMember(Name = "expires_in")]
        public string ExpiresIn { get; set; }

        [DataMember(Name = "resource")]
        public string Resource { get; set; }

        [DataMember(Name = "expires_on")]
        public string ExpiresOn { get; set; }
       
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        public AuthenticatedRefreshTokenResponse()
        {
        }
    }
}