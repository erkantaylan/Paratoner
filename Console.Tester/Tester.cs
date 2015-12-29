#region

using System;
using System.Data;
using System.Text;

using FastQueries;

using PsqlConnector;

#endregion

namespace Cmd.Tester {

    public static class Tester {
        private static void Main(string[] args) {
            var dbo = new DbOperations();
            var accountController = new AccountController(dbo);
            Console.WriteLine(accountController.Check("erkan", "1234"));

            Console.ReadKey();
        }

        private static string PrintTable(DataTable dt) {
            var print = new StringBuilder();
            if (dt.Rows.Count > 0) {
                for (var i = 0; i < dt.Columns.Count; i++) {
                    var columnName = dt.Columns[i].ColumnName;
                    print.Append($"| {columnName} ");
                }
                print.AppendLine("");
                for (var i = 0; i < dt.Rows.Count; i++) {
                    for (var j = 0; j < dt.Columns.Count; j++) {
                        var cell = dt.Rows[i][j].ToString();
                        print.Append($"{cell} | ");
                    }
                    print.AppendLine("");
                }
            }
            return print.ToString();
        }
    }

}