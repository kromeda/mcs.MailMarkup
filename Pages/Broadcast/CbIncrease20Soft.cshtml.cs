using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MailMarkup.Pages.Broadcast
{
    public class CbIncrease20SoftModel : PageModel
    {
        public void OnGet([FromServices] ILogger<CbIncrease20SoftModel> logger)
        {
            logger.LogInformation("«апрос разметки email письма дл€ рассылки о повышении ключевой ставки (верси€ дл€ добросовестных платильщиков).");
        }
    }
}