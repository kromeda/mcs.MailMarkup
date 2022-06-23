using Microsoft.EntityFrameworkCore;

namespace MailMarkup.DataAccess
{
    public class StekContext : DbContext
    {
        public StekContext(DbContextOptions options) : base(options)
        {

        }
    }
}
