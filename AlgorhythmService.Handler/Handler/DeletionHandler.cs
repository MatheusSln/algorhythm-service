using AlgorhythmService.Handler.Exercise.Repository;
using AlgorhythmService.Handler.Handler.Interface;
using AlgorhythmService.Shared;
using AlgorhythmService.Shared.UnitTest;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AlgorhythmService.Handler.Handler
{
    public class DeletionHandler : IDeletionHandler
    {
        private readonly int DEFAULT_DELAY = 40000;

        private readonly TimeSpan DEFAULT_INITIALIZE_TIME = new TimeSpan(01, 00, 00);

        private readonly AppSettingsRoot _appSettingsRoot;

        private readonly IExerciseRepository _exerciceRepository;

        public DeletionHandler(AppSettingsRoot appSettingsRoot, IExerciseRepository exerciceRepository)
        {
            _appSettingsRoot = appSettingsRoot;
            _exerciceRepository = exerciceRepository;
        }

        public async Task DeleteOldDataAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (!MustInitializaService())
                {
                    await Delay(stoppingToken);
                    continue;
                }

                var exercises = await _exerciceRepository.GetExercisesToDelete();

                if (exercises.Any())
                {
                    await _exerciceRepository.DeleteOldAlternativesByExercises(exercises);

                    await _exerciceRepository.DeleteOldExercisesRealizedByUsers(exercises);

                    await _exerciceRepository.DeleteOldExercicesAsync(exercises);
                }


                await Delay(stoppingToken);
            }
        }

        private bool MustInitializaService()
        {
            if (!TimeSpan.TryParse(_appSettingsRoot.InitServiceTimeSpanUTC, out var initServiceTime))
            {
                initServiceTime = DEFAULT_INITIALIZE_TIME;
            }

            var actualHour = DateTime.Now.TimeOfDay;

#if DEBUG
            initServiceTime = actualHour;
#endif

            return actualHour.Hours == initServiceTime.Hours && actualHour.Minutes == initServiceTime.Minutes;
        }

        private async Task Delay(CancellationToken stoppingToken)
        {
            if (!UnitTestDetector.IsRunningUnitTest)
                await Task.Delay(DEFAULT_DELAY, stoppingToken);
        }
    }
}
