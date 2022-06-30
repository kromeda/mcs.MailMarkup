namespace MailMarkup.Models
{
    public class MailMarkupConfiguration
    {
        public SeqConfiguration Seq { get; set; }

        public HostList Hosts { get; set; }

        public class HostList
        {
            public string DocumentsExternal { get; set; }
        }

        public class SeqConfiguration
        {
            public string Host { get; set; }

            public string ApiKey { get; set; }
        }
    }
}