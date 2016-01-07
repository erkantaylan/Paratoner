#region

using System.Data;

using PsqlConnector;

#endregion

namespace FastQueries.Select {

    public class GetGroupMembers {
        private DbOperations Dbo { get; set; }

        public GetGroupMembers(DbOperations dbo) {
            this.Dbo = dbo;
        }

        public DataTable SelectByGroupId(int groupId) {
            string query = $"SELECT * FROM view_users_with_group WHERE group_id={groupId};";
            return Dbo.SelectTable(query);
        }

        public DataTable SelectOnlyApporedByGroupId(int groupId) {
            string query = $"SELECT * FROM view_users_with_group WHERE group_id={groupId} AND is_approved=TRUE;";
            return Dbo.SelectTable(query);
        }
    }

}