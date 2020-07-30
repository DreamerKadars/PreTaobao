using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PreTaobao.Pages
{
    public class ExampleModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }
        public IActionResult OnGet()
        {
            
            return new RedirectToPageResult("Test");
        }
    }

}
