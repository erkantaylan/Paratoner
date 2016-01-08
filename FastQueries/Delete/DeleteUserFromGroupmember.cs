using System.Diagnostics;

using PsqlConnector;

namespace FastQueries.Delete {

    public class DeleteUserFromGroupmember {
        private readonly DbOperations _dbo;

        public DeleteUserFromGroupmember(DbOperations dbo) {
            this._dbo = dbo;
        }

        public bool DeleteViaGroupmemberId(int groupmemberId) {
            string query = $"DELETE FROM groupmember WHERE groupmember_id = {groupmemberId}";
            int rowCount = this._dbo.RunCommand(query);
            Debug.WriteLineIf(rowCount == -1, $"rowCount == -1,query:{query}");
            return rowCount != -1;
        }
    }

}