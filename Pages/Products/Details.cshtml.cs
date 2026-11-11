using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wt_lab12_12auth_validation_baranova.Data;
using wt_lab12_12auth_validation_baranova.Models;

namespace wt_lab12_12auth_validation_baranova.Pages.Products;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;

    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    public Product? Product { get; set; }

    // GET /Products/Details?id=1
    public async Task<IActionResult> OnGetAsync(int id)
    {
        Product = await _context.Products.FindAsync(id);

        if (Product == null)
        {
            return NotFound();
        }

        return Page();
    }
}
