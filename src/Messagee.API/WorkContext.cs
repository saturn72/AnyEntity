using System.Collections.Generic;

namespace Messagee.API
{
    public class WorkContext
    {
        public string CurrentUserId { get; set; }
        public string CurrentClientId { get; set; }
        public IEnumerable<string> CurrentRoles { get; set; }
        public IEnumerable<string> Namespaces { get; set; }
    }
}