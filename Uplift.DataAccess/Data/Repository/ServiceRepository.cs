using Uplift.DataAccess.Data.IRepository;
using Uplift.Models;
using UpliftApp.DataAccess.Data;

namespace Uplift.DataAccess.Data.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private readonly ApplicationDbContext _db;

        public ServiceRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
        public void Update(Service service)
        {
            var updatedService = _db.Services.Attach(service);
            updatedService.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
