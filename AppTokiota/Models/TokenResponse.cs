using System;
using System.Runtime.Serialization;

namespace AppTokiota.Models
{
    [DataContract]
    public class AuthenticatedUserResponse
    {
        private readonly DateTime _dateCreation;

        public AuthenticatedUserResponse()
        {
            _dateCreation = DateTime.Now;
        }

        [IgnoreDataMember]
        public DateTime Expiration { get => _dateCreation.AddSeconds(int.Parse(ExpiresIn)); }

        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }

        [DataMember(Name = "scope")]
        public string Scope { get; set; }

        [DataMember(Name = "expires_in")]
        public string ExpiresIn { get; set; }

        [DataMember(Name = "ext_expires_in")]
        public string ExtExpiresIn { get; set; }

        [DataMember(Name = "expires_on")]
        public string ExpiresOn { get; set; }

        [DataMember(Name = "not_before")]
        public string NotBefore { get; set; }

        [DataMember(Name = "resource")]
        public string Resource { get; set; }

        [DataMember(Name = "pwd_exp")]
        public string PwdExp { get; set; }

        [DataMember(Name = "pwd_url")]
        public string PwdUrl { get; set; }

        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }
    }
}
