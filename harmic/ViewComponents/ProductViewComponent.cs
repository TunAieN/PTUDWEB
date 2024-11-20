using harmic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace harmic.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly Harmic1Context _context;


        public ProductViewComponent(Harmic1Context context) {

            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = _context.TbProducts.Include(m => m.CategoryProduct)
.           Where(m => (bool)m.IsActive).Where(m => m.IsNew);
            return await Task.FromResult<IViewComponentResult>
            (View(items.OrderByDescending(m => m.ProductId).ToList()));
        }
     }
}
