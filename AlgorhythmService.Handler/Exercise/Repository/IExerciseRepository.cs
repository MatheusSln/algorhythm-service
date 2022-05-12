using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlgorhythmService.Handler.Exercise.Repository
{
    public interface IExerciseRepository
    {
        Task DeleteOldExercicesAsync(List<Guid> exercises);

        Task DeleteOldAlternativesByExercises(List<Guid> exercises);

        Task DeleteOldExercisesRealizedByUsers(List<Guid> exercises);

        Task<List<Guid>> GetExercisesToDelete();
    }
}
