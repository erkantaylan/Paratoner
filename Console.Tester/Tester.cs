﻿#region

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

            SelectUserGroupList userGroupList = new SelectUserGroupList(dbo);
            Console.WriteLine(PrintTable(userGroupList.SelectGroups(1)));

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