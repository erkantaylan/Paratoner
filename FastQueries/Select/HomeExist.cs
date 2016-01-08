using System;

using PsqlConnector;

namespace FastQueries.Select {

    public class HomeExist {

        private readonly DbOperations _dbo;

        public HomeExist(DbOperations dbo) {
            this._dbo = dbo;
        }

        public bool CheckName(string name) {
            var query = " SELECT name FROM \"group\" WHERE " +
                        $" name LIKE \'{name}\'";
            var dt = this._dbo.SelectTable(query);
            return dt.Rows.Count > 0;
        }

        public int GetGroupId(string name) {
            var query = " SELECT group_id FROM \"group\" WHERE " +
                        $" name LIKE \'{name}\'";
            var dt = this._dbo.SelectTable(query);
            int id = Convert.ToInt32(dt.Rows[0][0].ToString());
            return id;
        }

    }

}