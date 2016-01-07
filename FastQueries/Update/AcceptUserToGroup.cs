#region

using System.Diagnostics;

using PsqlConnector;

#endregion

namespace FastQueries.Update {

    public class AcceptUserToGroup {
        private DbOperations Dbo { get; set; }

        public AcceptUserToGroup(DbOperations dbo) {
            this.Dbo = dbo;
        }

        public bool UpdateWithGroupmemberId(int groupmemberId) {
            string query = $"UPDATE groupmember SET is_approved=TRUE WHERE groupmember_id={groupmemberId}";
            var rowCount = Dbo.RunCommand(query);
            Debug.WriteLineIf(rowCount > 0, $"(query):{query}");
            return rowCount > 0;
        }
    }

}