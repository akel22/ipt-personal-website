using System.Collections.Generic;
using System.Linq;

namespace VelascoPersonalWebsite_IPT.Application.Validation
{
    public class RegistrationValidationResult
    {
        public bool IsValid { get; private set; }

        public IReadOnlyCollection<string> Errors { get; private set; }

        private RegistrationValidationResult(bool isValid, IEnumerable<string> errors)
        {
            IsValid = isValid;
            Errors = errors.ToList().AsReadOnly();
        }

        public static RegistrationValidationResult Success()
        {
            return new RegistrationValidationResult(true, new string[0]);
        }

        public static RegistrationValidationResult Failure(IEnumerable<string> errors)
        {
            return new RegistrationValidationResult(false, errors);
        }
    }
}
