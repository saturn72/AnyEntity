using System.Threading.Tasks;

namespace Messagee.API.Services.Topics
{
    public interface ITokenGenerator
    {
        /// <summary>
        /// Generates token
        /// </summary>
        /// <returns>token</returns>
        public Task<object> Next();
    }
}
