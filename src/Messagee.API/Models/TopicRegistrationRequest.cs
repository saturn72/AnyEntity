namespace Messagee.API.Models
{
    public class TopicRegistrationRequest
    {
        /// <summary>
        /// Gets or set identifier sent by the requester to identifier topic registration request
        /// </summary>
        public string ReferenceId { get; set; }
        /// <summary>
        /// Gets or set Account name for topic
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// Gets or set topic namespace
        /// </summary>
        public string Namespace { get; set; }
        /// <summary>
        /// Gets or set topic-to-register name
        /// </summary>
        public string Topic { get; set; }
        /// <summary>
        /// Gets or set Registration type of topic (as pub, sub, ip etc.)
        /// </summary>
        public string PermissionType { get; set; }
    }
}
