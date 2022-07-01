using MailMarkup.Cache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

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

        public void OnGet([FromServices] ILogger<AllRecognizedModel> logger)
        {
            logger.LogInformation("Запрос разметки email письма для ответа на отправку показаний. Все файлы приняты.");
        }
    }
}
