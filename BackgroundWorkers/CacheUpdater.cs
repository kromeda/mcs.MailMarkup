using MailMarkup.Cache;
using MailMarkup.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MailMarkup.BackgroundWorkers
{
    internal class CacheUpdater : BackgroundService
    {
        private static readonly TimeSpan delay = TimeSpan.FromMinutes(5);
        private readonly IServiceProvider services;
        private readonly IServiceCache cache;
        private readonly ILogger<CacheUpdater> logger;

        public CacheUpdater(ILogger<CacheUpdater> logger, IServiceProvider services, IServiceCache cache)
        {
            this.services = services;
            this.cache = cache;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken _)
        {
            while (true)
            {
                try
                {
                    using IServiceScope scope = services.CreateScope();
                    var repository = scope.ServiceProvider.GetRequiredService<IStekRepository>();

                    await Task.WhenAll(new Task[]
                    {
                        Task.Run(async () => cache.OrganizationName = await repository.GetOrganizationName())
                    })
                    .ContinueWith(completedTask =>
                    {
                        switch (completedTask.Status)
                        {
                            case TaskStatus.RanToCompletion:
                                cache.IsInitialized = true;
                                break;
                            case TaskStatus.Faulted:
                                logger.LogError(completedTask.Exception, "При загрузке кэша произошла ошибка.");
                                break;
                            default:
                                break;
                        }
                    });
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "При загрузке кэша произошла ошибка.");
                }

                GC.Collect();
                await Task.Delay(delay);
            }
        }
    }
}
