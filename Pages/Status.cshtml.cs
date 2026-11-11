using Microsoft.AspNetCore.Mvc.RazorPages;

namespace wt_lab12_12auth_validation_baranova.Pages;

public class StatusModel : PageModel
{
    public int Code { get; set; }

    public void OnGet(int code)
    {
        Code = code;
        Response.StatusCode = code;
    }
}
