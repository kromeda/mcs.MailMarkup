using MailMarkup.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace MailMarkup.Pages.Avalanche
{
    public class NotificationFlModel : PageModel
    {
        private readonly MailMarkupConfiguration configuration;

        public NotificationFlModel(IOptions<MailMarkupConfiguration> options)
        {
            configuration = options.Value;
        }

        public string Link { get; set; }

        public IActionResult OnGet() => NotFound();

        public void OnPost([FromBody] InputModel input, [FromServices] ILogger<NotificationFlModel> logger)
        {
            Link = $"{configuration.Hosts.DocumentsExternal}/file/{input.Guid}";
            logger.LogInformation("Запрос разметки email письма для отправки уведомления.");
        }

        public class InputModel
        {
            [Required]
            public string Guid { get; set; }
        }
    }
}