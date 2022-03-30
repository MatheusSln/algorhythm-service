using AlgorhythmService.Handler.Exercice.Repository;
using AlgorhythmService.Shared.Database;
using Dapper;
using System;
using System.Threading.Tasks;

namespace AlgorhythmService.Data.Repository
{
    public class ExerciceRepository : IExerciceRepository
    {
        private DbSession _session;

        public ExerciceRepository(DbSession session)
        {
            _session = session;
        }

        public async Task DeleteOldExercicesAsync()
        {
            var lastTwoMonths = DateTime.Now.AddMonths(-2);

            await _session.OpenAsync();

            await _session.Connection.ExecuteAsync(
                    sql: $@"
                           DELETE FROM 
	                            FAKE_TABLE
                            WHERE
	                            FAKE_TABLE_Date < @lastTwoMonths
                        ",
                    param: new
                    {
                        lastTwoMonths
                    },
                    transaction: _session.Transaction
                    );
        }
    }
}
