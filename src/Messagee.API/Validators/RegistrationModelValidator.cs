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
            RuleFor(c => c.Topics).Must(
                x => x != null &&
                x.All(VerifyTopicRegistrationRequest));

        }

        protected bool VerifyTopicRegistrationRequest(TopicRegistrationRequest trr)
        {
            return 
                trr.Account.HasValue() &&
                trr.Namespace.HasValue() &&
                trr.Topic.HasValue() &&
                trr.ReferenceId.HasValue() &&
                trr.PermissionType.HasValue() &&
                TopicPermissionTypes.All.Contains(trr.PermissionType);
        }
    }
}
