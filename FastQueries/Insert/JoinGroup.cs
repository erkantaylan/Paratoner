using System.Diagnostics;

using PsqlConnector;

namespace FastQueries.Insert {
    /// <summary>
    /// This class add a member to group via userId and groupId
    /// </summary>
    public class JoinGroup {

        public JoinGroup(DbOperations dbo) {
            this.Dbo = dbo;
        }

        public DbOperations Dbo { get; set; }

        public bool Join(int userId, int groupId) {
            var query = " INSERT INTO public.groupmember( " +
                        " user_id, group_id, is_approved, is_admin) " +
                        $" VALUES ({userId}, {groupId}, FALSE, FALSE);";
            int rowCount = this.Dbo.RunCommand(query);
            Debug.WriteLineIf(rowCount == -1, $"JoinGroup.Join query:{query}");

            return rowCount > -1;
        }
        
    }

}