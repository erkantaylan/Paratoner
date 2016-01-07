using System.Data;
using System.Diagnostics;

using PsqlConnector;

namespace FastQueries.Select
{
    public class SpLoanList
    {
        private readonly DbOperations _dbo;

        public SpLoanList(DbOperations dbo) {
            this._dbo = dbo;
        }

        public DataTable SelectByGroupId(int groupId) {
            string query = $" SELECT  ui.name || ' ' || ui.lastname AS \"Name\" " +
                           $" ,sp.* " +
                           $" FROM sp_group_loan_list('1-1-1', {groupId}) AS sp " +
                           $" INNER JOIN userinfo AS ui ON sp.userid = ui.user_id " +
                           $" ORDER BY sp.userid;";
            Debug.WriteLine(query);
            return _dbo.SelectTable(query);
        }
    }
}
