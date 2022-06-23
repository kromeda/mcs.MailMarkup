namespace MailMarkup.Cache
{
    public interface IServiceCache
    {
        bool IsInitialized { get; set; }

        string OrganizationName { get; set; }
    }
}