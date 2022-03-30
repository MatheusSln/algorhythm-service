using System.Threading.Tasks;

namespace AlgorhythmService.Handler.Exercice.Repository
{
    public interface IExerciceRepository
    {
        Task DeleteOldExercicesAsync();
    }
}
