using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wt_lab12_12auth_validation_baranova.Data;
using wt_lab12_12auth_validation_baranova.Models;

namespace wt_lab12_12auth_validation_baranova.Pages.Products;

public class EditModel : PageModel
{
    private readonly AppDbContext _context;

    public EditModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Product Product { get; set; } = new();

    // GET /Products/Edit?id=1
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        Product = product;
        return Page();
    }

    // POST /Products/Edit?id=1
    public async Task<IActionResult> OnPostAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            Product.Id = id;
            return Page();
        }

        // переносим значения формы в отслеживаемую запись и сохраняем
        product.Name = Product.Name;
        product.Category = Product.Category;
        product.Price = Product.Price;
        product.IsAvailable = Product.IsAvailable;
        product.Description = Product.Description;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
