using System.Collections.Generic;

namespace Messagee.API.Models
{
    public class RegistrationModel
    {
        /// <summary>
        /// Gets or sets the namespace of the registration request
        /// </summary>
        public string Namespace { get; set; }
        public IEnumerable<TopicRegistration> Registrations { get; set; }
    }
}