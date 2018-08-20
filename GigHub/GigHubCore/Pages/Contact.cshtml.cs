using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GigHubCore.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Página de contato.";
        }
    }
}
