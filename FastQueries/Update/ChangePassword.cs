using System.Diagnostics;

using PsqlConnector;

namespace FastQueries.Update
{
    public class ChangePassword
    {
        private DbOperations Dbo { get; set; }

        public ChangePassword(DbOperations dbo) {
            this.Dbo = dbo;
        }

        public bool Do(int userId, string password) {
            var query = "UPDATE \"user\" SET " +
                           $"password=\'{password}\' " +
                           $"WHERE user_id={userId}";
            var rowCount = Dbo.RunCommand(query);

            Debug.WriteLine("\n\nChangePassword.Do");
            Debug.WriteLine($"query:{query}");
            Debug.WriteLineIf(rowCount == -1, "Query didnt work");
            Debug.WriteLineIf(rowCount == 0, "Query didnt change anything");
            Debug.WriteLineIf(rowCount > 1, "Query affect more than 1");
            Debug.WriteLine("\n\n");
            return rowCount > 0;
        }
    }
}
