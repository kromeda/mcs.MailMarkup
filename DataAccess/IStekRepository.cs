using System.Threading.Tasks;

namespace MailMarkup.DataAccess
{
    public interface IStekRepository
    {
        Task<string> GetOrganizationName();
    }
}
