using Uplift.DataAccess.Data.IRepository;
using Uplift.Models;
using UpliftApp.DataAccess.Data;

namespace Uplift.DataAccess.Data.Repository
{
    public class WebImageRepository : Repository<WebImages>, IWebImageRepository
    {
        private readonly ApplicationDbContext _db;

        public WebImageRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }





    }
}
