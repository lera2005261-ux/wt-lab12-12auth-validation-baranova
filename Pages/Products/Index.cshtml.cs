using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wt_lab12_12auth_validation_baranova.Data;
using wt_lab12_12auth_validation_baranova.Models;

namespace wt_lab12_12auth_validation_baranova.Pages.Products;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Product> Products { get; set; } = new();

    // GET /Products/Index?Search=ноут
    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [TempData]
    public string? Message { get; set; }

    public async Task OnGetAsync()
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(Search))
        {
            var text = Search.Trim().ToLower();
            query = query.Where(p => p.Name.ToLower().Contains(text));
        }

        Products = await query
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    // POST /Products/Index?handler=Delete&id=7
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        Message = $"Товар «{product.Name}» удалён.";
        return RedirectToPage("./Index");
    }
}
