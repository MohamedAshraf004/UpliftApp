using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Uplift.DataAccess.Data.IRepository;
using Uplift.Models;
using UpliftApp.DataAccess.Data;

namespace Uplift.DataAccess.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }


        public IEnumerable<SelectListItem> GetCategoryListForDropDown()
        {
            return _db.Categories.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
        }

        public void Update(Category category)
        {
            var updatedCategory = _db.Categories.Attach(category);
            updatedCategory.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
