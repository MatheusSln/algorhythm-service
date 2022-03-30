using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AlgorhythmService.Shared.Database
{
    public class DbSession : IDisposable
    {
        public SqlConnection Connection { get; private set; }
        public IDbTransaction Transaction { get; set; }

        public DbSession(AppSettingsRoot appSettingsRoot)
        {
            var conn = appSettingsRoot.ConnectionString;

            Connection = new SqlConnection(conn);
        }

        public async Task OpenAsync()
        {
            if (Connection.State != ConnectionState.Open)
                await Connection.OpenAsync();
        }

        public async Task CloseAsync()
        {
            await Connection?.CloseAsync();
        }

        public void Dispose()
        {
            Connection?.Close();
        }
    }
}
