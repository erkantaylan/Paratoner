#region

using System;
using System.Data;
using System.Globalization;
using System.Text;

using FastQueries;
using FastQueries.Select;
using FastQueries.Update;

using PsqlConnector;

#endregion

namespace Cmd.Tester {

    public static class Tester {
        private static void Main(string[] args) {
            var dbo = new DbOperations();
            //var accountController = new AccountController(dbo);
            //var timeUpdater = new UpdateUserLoginTime(dbo);
            //var id = accountController.Check("erkan", "1234");
            //Console.WriteLine($"Account Check:{id}");

            //Console.WriteLine($"Update Time:{timeUpdater.UpdateTime(id, DateTime.Now.ToString(CultureInfo.InvariantCulture))}");

            //SelectUserGroupList userGroupList = new SelectUserGroupList(dbo);
            //Console.WriteLine(PrintTable(userGroupList.SelectGroups(1)));

            //CheckPassword cp = new CheckPassword(dbo);
            //ChangePassword cp2 = new ChangePassword(dbo);
            //Console.WriteLine(cp.Check(1, "12345"));
            //Console.WriteLine(cp2.Do(1, "1234"));

            //var sp = new SpLoanList(new DbOperations());
            //Console.WriteLine(PrintTable(sp.SelectByGroupId(1)));

            GetGroupMembers ggm = new GetGroupMembers(dbo);
            var dt = ggm.SelectByGroupId(1);
            Console.WriteLine(PrintTable(dt));

            Console.Write("FIN...");
            Console.ReadKey();
        }

        private static string PrintTable(DataTable dt) {
            var print = new StringBuilder();
            if (dt.Rows.Count > 0) {
                for (var i = 0; i < dt.Columns.Count; i++) {
                    var columnName = dt.Columns[i].ColumnName;
                    print.Append($"{columnName,-20}");
                }
                print.AppendLine("");
                for (var i = 0; i < dt.Rows.Count; i++) {
                    for (var j = 0; j < dt.Columns.Count; j++) {
                        var cell = dt.Rows[i][j].ToString();
                        print.Append($"{cell,-20}");
                    }
                    print.AppendLine("");
                }
            }
            return print.ToString();
        }


    }

}