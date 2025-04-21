using Microsoft.AspNetCore.Mvc.RazorPages;

namespace T1_PR2_Client.Pages
{
    public class UsersChatModel : PageModel
    {
        public bool IsAuthenticated { get; private set; }
        public void OnGet()
        {
            IsAuthenticated = User.Identity?.IsAuthenticated ?? false;
        }
    }
}
