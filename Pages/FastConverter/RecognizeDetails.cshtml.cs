using MailMarkup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace MailMarkup.Pages.FastConverter
{
    public class RecognizeDetailsModel : PageModel
    {
        public bool ExternalSend { get; set; }

        public List<string> Recognized { get; set; }

        public List<string> Unrecognized { get; set; }

        public void OnPost([FromBody] RecognizeDetailsModel input, [FromServices] ILogger<RecognizeDetailsViewModel> logger)
        {
            ExternalSend = input.ExternalSend;
            Recognized = input.Recognized;
            Unrecognized = input.Unrecognized;

            logger.LogInformation("Запрос разметки email письма для ответа на отправку показаний. Есть непринятые файлы (детали).");
        }
    }
}