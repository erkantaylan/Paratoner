﻿#region

using System.Data;

using PsqlConnector;

#endregion

namespace FastQueries.Select {

    public class SelectUserGroupList {
        private DbOperations Dbo { get; }

        public SelectUserGroupList(DbOperations dbo) {
            this.Dbo = dbo;
        }

        public DataTable SelectGroups() {
            var query = "SELECT * FROM view_memberofgroups";
            var table = this.Dbo.SelectTable(string.Format(query));
            return table;
        }

        public DataTable SelectGroups(int userId) {
            /*
            CREATE OR REPLACE VIEW public.view_memberofgroups AS 
            SELECT "group".group_id,
	        "group".name,
	        "group".cutoff_day,
	        groupmember.is_approved,
	        groupmember.is_admin,
	        "user".user_id
            FROM "group"
            JOIN groupmember ON "group".group_id = groupmember.group_id
            JOIN "user" ON groupmember.user_id = "user".user_id;
            */
            var query = $"SELECT * FROM view_groupsofmember WHERE user_id={userId}";
            var table = this.Dbo.SelectTable(string.Format(query));
            return table;
        }

        public DataTable SelectGroupsOnlyAsAdmin(int userId) {
            var query = $"SELECT * FROM view_groupsofmember WHERE user_id={userId} AND is_admin=TRUE;";
            var table = this.Dbo.SelectTable(string.Format(query));
            return table;
        }
    }

}