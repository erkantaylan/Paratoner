#region

using System;
using System.Diagnostics;

using DatabaseClasses;

using PsqlConnector;

#endregion

namespace FastQueries.Insert {

    public class AddNewInvoice {
        private readonly DbOperations _dbo;
        private Invoice _invoice;

        public AddNewInvoice(DbOperations dbo) {
            this._dbo = dbo;
        }

        public bool Insert(Invoice invoice) {
            this._invoice = invoice;
            if (this._invoice == null) {
                throw new ArgumentNullException($"Invoice cannot be null or empty:{this._invoice}");
            }
            var command = "INSERT INTO public.invoice("
                          + "user_id, group_id, buy_date, is_expired, price)"
                          + "VALUES ("
                          + $"   {this._invoice.UserId}"
                          + $",  {this._invoice.GroupId}"
                          + $",\'{this._invoice.BuyDate}\'"
                          + $",\'{this._invoice.IsExpired}\'"
                          + $",  {this._invoice.Price});";
#if DEBUG
            Debug.WriteLine(command);
#endif
            return this._dbo.RunCommand(command) != -1;
        }

        public int GetLastInvoiceId() {
            /*  SELECT invoice_id, user_id, group_id, buy_date, is_expired, price
                FROM public.invoice;*/
            var query = "SELECT invoice_id FROM public.invoice WHERE " +
                        $" user_id={this._invoice.UserId}  AND " +
                        $" group_id={this._invoice.GroupId} AND " +
                        $" buy_date=\'{this._invoice.BuyDate}\' AND " +
                        $" is_expired=\'{this._invoice.IsExpired}\' AND " +
                        $" price=\'{this._invoice.Price}\' " +
                        " ORDER BY " +
                        " invoice_id DESC";
            
            var dt = this._dbo.SelectTable(query);

            if (dt == null) {
                Debug.WriteLine($"\nAddnewInvoice.GetInvoiceId\n");
                Debug.WriteLine($"query:{query}\n");
                return -1;
            }
            if (dt.Rows.Count == 0) {
                Debug.WriteLine($"\nAddnewInvoice.GetInvoiceId\n");
                Debug.WriteLine($"query:{query}\n");
                return 0;
            }
            int invoiceId = Convert.ToInt32(dt.Rows[0][0].ToString());
            return invoiceId;
        }
    }

}