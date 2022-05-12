using AlgorhythmService.Handler.Exercise.Repository;
using AlgorhythmService.Shared.Database;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlgorhythmService.Data.Repository
{
    public class ExerciseRepository : IExerciseRepository
    {
        private DbSession _session;

        public ExerciseRepository(DbSession session)
        {
            _session = session;
        }

        public async Task DeleteOldExercicesAsync(List<Guid> exercises)
        {
            await _session.OpenAsync();

            await _session.Connection.ExecuteAsync(
                    sql: $@"
                           DELETE FROM 
	                            Exercises
                            WHERE
	                            Id in @exercises
                        ",
                    param: new
                    {
                        exercises
                    },
                    transaction: _session.Transaction
                    );
        }

        public async Task DeleteOldAlternativesByExercises(List<Guid> exercises)
        {

            await _session.OpenAsync();

            await _session.Connection.ExecuteAsync(
                    sql: $@"
                           DELETE FROM 
	                            Alternatives
                            WHERE
	                            ExerciseId in @exercises
                        ",
                    param: new
                    {
                        exercises
                    },
                    transaction: _session.Transaction
                    );
        }

        public async Task DeleteOldExercisesRealizedByUsers(List<Guid> exercises)
        {

            await _session.OpenAsync();

            await _session.Connection.ExecuteAsync(
                    sql: $@"
                           DELETE FROM 
	                            ExerciseUser
                            WHERE
	                            ExercisesId in @exercises
                        ",
                    param: new
                    {
                        exercises
                    },
                    transaction: _session.Transaction
                    );
        }

        public async Task<List<Guid>> GetExercisesToDelete()
        {
            var lastTwoMonths = DateTime.Now.AddMonths(-2);

            await _session.OpenAsync();

            var result = await _session.Connection.QueryAsync<Guid>(
                                 sql: $@"
                                       SELECT 
                                            Id
                                       FROM
                                            Exercises
                                       WHERE
                                            DeletedAt < @lastTwoMonths
                                       ",
                                 param: new
                                 {
                                     lastTwoMonths
                                 });

            return result.ToList();
        }
    }
}
