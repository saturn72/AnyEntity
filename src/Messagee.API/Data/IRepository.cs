using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messagee.API.Data
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetAll();
    }
}
