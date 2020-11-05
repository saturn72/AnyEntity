using FluentValidation.TestHelper;
using Messagee.API.Models;
using Messagee.API.Validators;
using System.Threading.Tasks;
using Xunit;

namespace Messagee.API.Tests.Validators
{
    public class RegistrationModelValidatorTests
    {
        [Fact]
        public async Task TopicNames_Rules()
        {
            var v = new RegistrationModelValidator();
            var cd = new RegistrationModel();
            var res = await v.TestValidateAsync(cd);
            res.ShouldHaveValidationErrorFor(c => c.Registrations);
            res.ShouldHaveValidationErrorFor(c => c.Namespace);
        }
    }
}
