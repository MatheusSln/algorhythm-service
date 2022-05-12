using AlgorhythmService.Handler.Handler.Interface;
using AlgorhythmService.Shared.LogShared;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AlgorhythmService
{
    public class Worker : BackgroundService
    {
        private readonly IDeletionHandler _deletionHandler;

        public Worker(IDeletionHandler deletionHandler)
        {
            _deletionHandler = deletionHandler;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            LogShared.LogInformationAsync("---- StartAsync now ----").Wait();

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            LogShared.LogInformationAsync("---- StopAsync now ----").Wait();
            LogShared.LogInformationAsync("---- Service ended ----").Wait();

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await _deletionHandler.DeleteOldDataAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                await LogShared.LogErrorAsync(ex);
            }
        }
    }
}
