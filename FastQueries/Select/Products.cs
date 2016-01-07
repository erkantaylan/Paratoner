using System.Collections.Generic;
using System.Data;

using DatabaseClasses;

using PsqlConnector;

namespace FastQueries.Select
{
    public class Products
    {
        private DbOperations Dbo { get; set; }

        public Products(DbOperations dbo) {
            this.Dbo = dbo;
        }

        public DataTable GetAsTable(int invoiceId) {
            var query = $"SELECT name FROM product WHERE invoice_id = {invoiceId}";
            var dt = Dbo.SelectTable(query);
            return dt;
        }

        public List<Product> TableToList(DataTable dt) {
            if (dt?.Rows.Count > 0) {
                List<Product> list = new List<Product>(dt.Rows.Count);
                for (int i = 0; i < dt.Rows.Count; i++) {
                    list.Add(
                        new Product {
                                        Name = $"{dt.Rows[i][0].ToString().ToUpper()}"
                                    });
                }
                return list;
            }
            return null;
        }
    }
}
