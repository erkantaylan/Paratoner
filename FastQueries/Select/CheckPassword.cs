#region

using PsqlConnector;

#endregion

namespace FastQueries.Select {

    public class CheckPassword {
        private DbOperations Dbo { get; set; }

        public CheckPassword(DbOperations dbo) {
            this.Dbo = dbo;
        }

        public bool Check(int userId, string password) {
            string query = $"SELECT password FROM \"user\" WHERE user_id={userId} AND password =\'{password}\'";
            var dt = Dbo.SelectTable(query);
            return dt.Rows.Count > 0;
        }
    }

}