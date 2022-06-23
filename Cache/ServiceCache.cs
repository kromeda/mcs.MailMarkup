namespace MailMarkup.Cache
{
    public class ServiceCache : IServiceCache
    {
        public bool IsInitialized { get; set; }

        public string OrganizationName { get; set; }
    }
}