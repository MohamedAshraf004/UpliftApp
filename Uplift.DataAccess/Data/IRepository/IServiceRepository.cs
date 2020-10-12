using Uplift.Models;

namespace Uplift.DataAccess.Data.IRepository
{
    public interface IServiceRepository : IRepository<Service>
    {
        void Update(Service service);
    }
}
