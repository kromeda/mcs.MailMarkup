using MailMarkup.Cache;
using MailMarkup.DataAccess;
using MailMarkup.Exceptinos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MailMarkup.BackgroundWorkers
{
    internal class CacheUpdater : BackgroundService
    {
        private static readonly TimeSpan delay = TimeSpan.FromMinutes(1);
        private readonly IServiceProvider services;
        private readonly IServiceCache cache;
        private readonly IExceptionHandler exceptions;

        public CacheUpdater(IServiceProvider services, IServiceCache cache)
        {
            this.services = services;
            this.cache = cache;
            using IServiceScope scope = services.CreateScope();
            exceptions = scope.ServiceProvider.GetRequiredService<IExceptionHandler>();
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
                                exceptions.Handle(completedTask.Exception);
                                break;
                            default:
                                break;
                        }
                    });
                }
                catch (Exception ex)
                {
                    exceptions.Handle(ex);
                }

                GC.Collect();
                await Task.Delay(delay);
            }
        }
    }
}
