using Microsoft.AspNetCore.Mvc;
using harmic.Models;
using Microsoft.EntityFrameworkCore;


namespace harmic.Controllers
{
	public class BlogController : Controller
	{
		private readonly Harmic1Context _context;
		public BlogController(Harmic1Context context)
		{
			_context = context;
		}
		[Route("/blog/{alias}-{id}.html")]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.TbBlogs == null)
			{
				return NotFound();
			}

			var blog = await _context.TbBlogs
				.FirstOrDefaultAsync(m => m.BlogId == id);

			if (blog == null)
			{
				return NotFound();
			}

			ViewBag.blogcomment = _context.TbBlogComments.
				Where(i => i.BlogId == id ).ToList();
			


			return View(blog);
		}

	}
}
