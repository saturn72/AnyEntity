using System;
using System.Collections.Generic;

namespace Messagee.API.Models
{
    public class RegistrationModel
    {
        public IEnumerable<TopicRegistrationRequest> Topics { get; set; }

        internal bool All(object verifyTopicRegistrationRequest)
        {
            throw new NotImplementedException();
        }
    }
}