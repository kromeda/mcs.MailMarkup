using MailMarkup.Cache;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MailMarkup.Pages.FastConverter
{
    public class AllRecognizedModel : PageModel
    {
        private readonly IServiceCache cache;

        public AllRecognizedModel(IServiceCache cache)
        {
            this.cache = cache;
        }

        public string OrganizationName => cache.OrganizationName;

        public void OnGet()
        {

        }
    }
}
