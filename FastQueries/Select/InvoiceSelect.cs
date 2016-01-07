#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using DatabaseClasses;

using PsqlConnector;

#endregion

namespace FastQueries.Select {

    public class InvoiceSelect {
        private DbOperations Dbo { get; set; }

        public InvoiceSelect(DbOperations dbo) {
            this.Dbo = dbo;
        }

        public List<Invoice> TableToList(DataTable dt) {
            var iList = new List<Invoice>();
            if (dt?.Rows.Count > 0) { 
                for (int i = 0; i < dt.Rows.Count; i++) {
                    string name      =                  dt.Rows[i][0].ToString();
                    int invoiceId    = Convert.ToInt32( dt.Rows[i][1].ToString());
                    int userId       = Convert.ToInt32( dt.Rows[i][2].ToString());
                    int groupId      = Convert.ToInt32( dt.Rows[i][3].ToString());
                    string buyDate   =                  dt.Rows[i][4].ToString();
                    double price     = Convert.ToDouble(dt.Rows[i][5].ToString());
                    var inv = new Invoice {
                                              UserName = name,
                                              InvocieId = invoiceId,
                                              UserId = userId,
                                              GroupId = groupId,
                                              BuyDate = buyDate,
                                              Price = price
                                          };
                    Products p = new Products(new DbOperations());
                    var productList = p.TableToList(p.GetAsTable(invoiceId));
                    inv.ProductList = productList.ToList();
                    iList.Add(inv);
                }
            }
            return iList;
        }

        public DataTable SelectAsTable(string query) {
            return Dbo.SelectTable(query);
        }

        /// <summary>
        /// Select by groupId
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public DataTable SelectAsTable(int groupId) {
            string query = $"SELECT * FROM view_get_invoices_with_name WHERE group_id={groupId}";
            return Dbo.SelectTable(query);
        }
    }

}