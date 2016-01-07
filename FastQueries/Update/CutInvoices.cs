#region

using System.Diagnostics;

using PsqlConnector;

#endregion

namespace FastQueries.Update {

    public class CutInvoices {
        private DbOperations Dbo { get; }

        public CutInvoices(DbOperations dbo) {
            this.Dbo = dbo;
        }

        public bool CutByGroupId(int groupId) {
            string query = $"DELETE FROM invoice WHERE group_id = {groupId}";
            Debug.WriteLine(query);
            var rowCount = Dbo.RunCommand(query);
            return rowCount > 0;
        }
    }

}