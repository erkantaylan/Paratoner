using System.Diagnostics;

using DatabaseClasses;

using PsqlConnector;

namespace FastQueries.Insert {

    public class AddNewGroup {

        private readonly DbOperations _dbo;

        public AddNewGroup(DbOperations dbo) {
            this._dbo = dbo;
        }

        public bool Add(Group g) {
            string query = $" INSERT INTO \"group\" (name,cutoff_day) " +
                           $" VALUES(\'{g.Name}\', {g.CutOffDate}) ";
            int rowCount = this._dbo.RunCommand(query);
            Debug.WriteLineIf(rowCount != -1, $"AddNewGroup.Add query:{query}");
            return rowCount != -1;
        }

    }

}