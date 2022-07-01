using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MailMarkup.Pages.Broadcast
{
    public class CbIncrease20HardModel : PageModel
    {
        public void OnGet([FromServices] ILogger<CbIncrease20HardModel> logger)
        {
            logger.LogInformation("������ �������� email ������ ��� �������� � ��������� �������� ������ (������ ��� ���������).");
        }
    }
}
