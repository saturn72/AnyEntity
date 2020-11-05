using System.Threading.Tasks;

namespace Messagee.API.Services.Security
{
    public interface IPermissionManager
    {
        Task<bool> UserPermittedForNamespace(string @namespace);
    }
}
