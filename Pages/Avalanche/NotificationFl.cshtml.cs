using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MailMarkup.Pages.Avalanche
{
    public class NotificationFlModel : PageModel
    {
        public string Link { get; set; }

        public void OnPost([FromBody] InputModel input)
        {
            Link = $"https://localhost:5001/api/v1/docs/fl/notifications/view/{input.Guid}";
        }

        public class InputModel
        {
            [Required]
            public string Guid { get; set; }
        }
    }
}