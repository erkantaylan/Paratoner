#region

using System.Data;

using Npgsql;

#endregion

namespace PsqlConnector {

    public class DbOperations : DbConnect {
        public DbOperations(string database, string hostname, string username, string password, string port)
            : base(hostname, username, password, port, database) { }

        public DbOperations() { }

        public DataTable SelectTable(string query) {
            this.InitializeConnection();
            var sqlCmd = new NpgsqlCommand(query, this._conn);
            var sqlDa = new NpgsqlDataAdapter(sqlCmd);
            var dt = new DataTable();
            try {
                this.OpenConnection();
                sqlDa.Fill(dt);
            }
            catch {
                // ignored
            }
            finally {
                this.CloseConnection();
            }
            return dt;
        }

        /// <summary>
        /// </summary>
        /// <param name="query">postgresql querry</param>
        /// <returns>Number of affected rows</returns>
        public int RunCommand(string query) {
            var numberOfRows = 0;
            this.InitializeConnection();
            var sqlCmd = new NpgsqlCommand(query, this._conn);
            try {
                this.OpenConnection();
                numberOfRows = sqlCmd.ExecuteNonQuery();
            }
            catch {
                numberOfRows = -1;
            }
            finally {
                this.CloseConnection();
            }
            return numberOfRows;
        }
    }

}