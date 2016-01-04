using System.Collections.Generic;
using System.Diagnostics;

using DatabaseClasses;

using PsqlConnector;

namespace FastQueries.Insert
{
    public class AddNewProduct
    {
        private DbOperations Dbo { get; set; }

        public AddNewProduct(DbOperations dbo ) {
            this.Dbo = dbo;
        }

        public bool Insert(Product product, int invoiceId) {
            
            /*
            INSERT INTO public.product(
            product_id, invoice_id, name)
            VALUES (?, ?, ?);
            */
            string query = "INSERT INTO public.product(invoice_id, name) VALUES(" +
                           $"{invoiceId}, \'{product.Name}\');";
            int rowCount = Dbo.RunCommand(query);
            if (rowCount == -1) {
                Debug.WriteLine(
                    "error: AddNewProducut.Insert" +
                    $"query:{query}\n" +
                    $"product.id:{product.ProductId}\n" +
                    $"invoice.id:{invoiceId}\n");
                return false;
            }
            return true;
        }

        public bool Insert(List<Product> productList, int invoiceId) {
            for (int i = 0; i < productList.Count; i++) {
                if (!this.Insert(productList[i], invoiceId)) {
                    //eger Insert ten false donerse islemi yarida kes ve bilgilendir
                    return false;
                }
            }
            return true;
        }
    }
}
