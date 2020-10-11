using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Uplift.Models;

namespace Uplift.DataAccess.Data.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<SelectListItem> GetCategoryListForDropDown();

        void Update(Category category);
    }
}
