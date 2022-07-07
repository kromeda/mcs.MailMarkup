using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Threading.Tasks;

namespace MailMarkup.DataAccess
{
    public class StekRepository : IStekRepository
    {
        private readonly StekContext context;

        public StekRepository(StekContext context) => this.context = context;

        public async Task<string> GetOrganizationNameAsync()
        {
            using DbCommand command = context.Database.GetDbConnection().CreateCommand();

            command.CommandText = @"
            declare @OrganizationName nvarchar(max) = 
            (
                select
                    Название 
                from [stack].[Организации] 
                where row_id = 
                (
                    select top(1) 
                        Значение
                    from [stack].[Конфигурация] as K with (nolock)
                    where K.Сессия is null 
                        and K.Ключ = 'Id_Refer'
                )
            );
            set @OrganizationName = isnull(@OrganizationName, 'ПАО ""ТНС Энерго""');
            select @OrganizationName;";

            await command.Connection.OpenAsync();
            string organizationName = (string)await command.ExecuteScalarAsync();

            return organizationName;
        }
    }
}
