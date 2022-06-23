using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace MailMarkup.Pages.FastConverter
{
    public class RecognizeDetailsModel : PageModel
    {
        public bool ExternalSend { get; set; }

        public List<string> Recognized { get; set; }

        public List<string> Unrecognized { get; set; }

        public void OnPost([FromBody] RecognizeDetailsModel input)
        {
            ExternalSend = input.ExternalSend;
            Recognized = input.Recognized;
            Unrecognized = input.Unrecognized;
        }
    }
}
