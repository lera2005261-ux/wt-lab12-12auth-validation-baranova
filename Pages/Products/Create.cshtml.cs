using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wt_lab12_12auth_validation_baranova.Data;
using wt_lab12_12auth_validation_baranova.Models;

namespace wt_lab12_12auth_validation_baranova.Pages.Products;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Product Product { get; set; } = new();

    // GET /Products/Create — пустая форма
    public void OnGet()
    {
    }

    // POST /Products/Create — приём данных формы
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Product.Id = 0;   // Id создаёт PostgreSQL
        _context.Products.Add(Product);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
