using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

using DatabaseClasses;

using PsqlConnector;

namespace FastQueries.Select {

    public class GroupsWithoutUser {

        private readonly DbOperations _dbo;

        public GroupsWithoutUser(DbOperations dbo) {
            this._dbo = dbo;
        }

        public DataTable GetAsTable(int userId) {
            var query = $" SELECT g.group_id,g.name, g.cutoff_day FROM groupmember gm " +
                        $" INNER JOIN \"group\" AS g ON  g.group_id = gm.group_id " +
                        $" WHERE gm.group_id " +
                        $" NOT IN (SELECT group_id FROM groupmember WHERE user_id = {userId});";
            Debug.WriteLine("GroupsWtihoutUser.GetAsTable");
            Debug.WriteLine($"query:{query}");
            return this._dbo.SelectTable(query);
        }

        public List< Group > TableToList(DataTable dt) {
            if ( dt == null ) {
                return null;
            }
            List< Group > list = new List< Group >(dt.Rows.Count);
            for ( int i = 0; i < dt.Rows.Count; i++ ) {
                Group g = new Group {
                    GroupId = int.Parse(dt.Rows[i][0].ToString()),
                    Name = dt.Rows[i][1].ToString(),
                    CutOffDate = int.Parse(dt.Rows[i][2].ToString())
                };
                list.Add(g);
            }
            return list;
        }

    }

}