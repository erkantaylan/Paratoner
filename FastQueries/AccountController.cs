#region

using System;

using PsqlConnector;

#endregion

namespace FastQueries {

    public class AccountController {
        private DbOperations Dbo { get; }

        public AccountController(DbOperations dbo) {
            this.Dbo = dbo;
        }

        public int Check(string username, string password) {
            var loginQuery = "SELECT user_id"
                             + " FROM \"user\""
                             + $" WHERE username=\'{username}\' AND password=\'{password}\'";

            var table =  Dbo.SelectTable(string.Format(loginQuery));
            if (!(table.Rows.Count > 0)) {
                return -1;
            }
            var id = Convert.ToInt32(table.Rows[0][0].ToString());
            return id;

        }
    }

}