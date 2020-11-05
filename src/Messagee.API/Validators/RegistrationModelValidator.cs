using FluentValidation;
using Messagee.API.Models;
using Messagee.API.Services.Topics;
using System;
using System.Linq;

namespace Messagee.API.Validators
{
    public class RegistrationModelValidator : AbstractValidator<RegistrationModel>
    {
        public RegistrationModelValidator()
        {
            RuleFor(c => c.Registrations).Must(
                x => x != null &&
                x.All(d => d.TopicName.HasValue() && TopicRegistrationTypes.All.Contains(d.RegistrationType)));

            RuleFor(c => c.Namespace).Must(d => d.HasValue());
        }
    }
}
