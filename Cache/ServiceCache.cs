using System.Threading;

namespace MailMarkup.Cache
{
    public class ServiceCache : IServiceCache
    {
        private string organizationName;

        public bool IsInitialized { get; set; }

        public string OrganizationName
        {
            get => organizationName;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Interlocked.Exchange(ref organizationName, value);
                }
            }
        }
    }
}