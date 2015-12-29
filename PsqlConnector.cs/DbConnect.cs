#region

using Npgsql;

#endregion

namespace PsqlConnector {

    public class DbConnect {
        private readonly string _database;
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _port;
        private readonly string _username;
        protected NpgsqlConnection _conn;

        protected DbConnect(string hostname, string username, string password, string port, string database) {
            this._hostname = hostname;
            this._username = username;
            this._password = password;
            this._port     = port;
            this._database = database;
        }

        protected DbConnect() {
            this._hostname = "localhost";
            this._username = "postgres";
            this._password = "pgsofg41DC";
            this._port     = "5432";
            this._database = "ev_fatura";
        }

        protected void InitializeConnection() {
            var query = $"SERVER={this._hostname};"
                        + $"PORT={this._port};"
                        + $"USER ID={this._username};"
                        + $"PASSWORD={this._password};"
                        + $"DATABASE={this._database};";
            this._conn = new NpgsqlConnection(query);
        }

        protected void OpenConnection() {
            this._conn.Open();
        }

        protected void CloseConnection() {
            this._conn.Close();
        }
    }

}