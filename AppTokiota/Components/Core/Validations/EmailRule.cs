using System;
using EmailAddressLibrary;
namespace AppTokiota.Components.Core.Validations
{
    public class EmailRule : IValidationRule<string>
    {
        public EmailRule()
        {
            ValidationMessage = "Should be an email address";
        }

        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            return EmailAddressValidator.Msdn(value);
        }
    }
}
