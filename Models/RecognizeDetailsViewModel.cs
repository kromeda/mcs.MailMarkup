using System.Collections.Generic;

namespace MailMarkup.Models
{
    public class RecognizeDetailsViewModel
    {
        public bool ExternalSend { get; set; }

        public List<string> Recognized { get; set; }

        public List<string> Unrecognized { get; set; }
    }
}
