using System;
namespace AppTokiota.Users.Components.Core.Validations
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
            return true;//EmailAddressValidator.Msdn(value);
        }
    }
}
