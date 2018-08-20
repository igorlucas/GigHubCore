using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GigHubCore.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Descrição da aplicação.";
        }
    }
}
