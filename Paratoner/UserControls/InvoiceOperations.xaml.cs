#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using DatabaseClasses;

using FastQueries.Select;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

using PsqlConnector;

#endregion

namespace Paratoner.UserControls {

    public partial class InvoiceOperations : UserControl {
        private readonly int _userId;
        private List<GroupsOfMember> _groupList;
        private List<Product> _productList;

        public InvoiceOperations(int userId) {
            this._userId = userId;
            InitializeComponent();
        }

        private void cbxGroupList_OnLoaded(object sender, RoutedEventArgs e) {
            this.LoadGroupList();
        }

        private void LoadGroupList()
        {
            _groupList = SelectGroupsOfMemberById(this._userId);
            _groupList.Insert(
                0,
                new GroupsOfMember
                {
                    Name = "Select a group...",
                    ForegroundBrush = Brushes.Black
                });

            this.cbxGroupList.ItemsSource = _groupList;
            this.cbxGroupList.SelectedIndex = 0;
        }

        private List<GroupsOfMember> SelectGroupsOfMemberById(int userId)
        {
            var userGroupList = new SelectUserGroupList(new DbOperations());
            return GroupsOfMember.TableToList(userGroupList.SelectGroups(userId));
        }

        private void btnSearchInvoice_Click(object sender, RoutedEventArgs e) {
            if (cbxGroupList.SelectedIndex > 0) {
                int userId = this._userId;
                int groupId = _groupList[cbxGroupList.SelectedIndex].GroupId;
                string query = @" SELECT ui.name || ' ' || ui.lastname AS fullname " +
                               " ,i.invoice_id" +
                               " ,i.user_id" +
                               " ,i.group_id" +
                               " ,i.buy_date" +
                               " ,i.price " +
                               " FROM invoice AS i " +
                               " INNER JOIN userinfo AS ui ON ui.user_id = i.user_id " +
                               " WHERE i.is_expired = FALSE ";
                if (this.tsDate.IsChecked == true) {
                    var dt1 = dpSmall.SelectedDate;
                    var dt2 = dpBig.SelectedDate;
                    if ( (dt1 != null  && dt2 != null) && (dt1 > dt2) ) {
                        MessageBox("SEARCH", "LEFT SIDE DATE CANNOT BE BIGGER");
                    }
                    else {
                        query += $" AND i.buy_date BETWEEN " +
                                 $" \'{dt1.Value.Date.ToString(CultureInfo.InvariantCulture)}\' " +
                                 $" AND " +
                                 $" \'{dt2.Value.Date.ToString(CultureInfo.InvariantCulture)}\' ";
                    }
                }
                if (this.tsPrice.IsChecked == true) {
                    double price1;
                    double price2;
                    if (double.TryParse(txtPriceSmall.Text, out price1)) {
                        if (double.TryParse(this.txtPriceBig.Text, out price2)) {
                            if (!( price1 > price2 )) {
                                query += " AND " +
                                         " i.price BETWEEN " +
                                         $" CAST({price1} AS money) " +
                                         $" AND " +
                                         $" CAST({price2} AS money) ";
                            }
                            else {
                                MessageBox("SEARCH", "ERROR:LEFT SIDE PRICE CANNOT BE BIGGER");
                            }
                        }
                        else {
                            MessageBox("SEARCH", "ERROR:RIGHT SIDE PRICE NOT VALID");
                        }
                    }
                    else {
                        MessageBox("SEARCH", "ERROR:LEFT SIDE PRICE NOT VALID");
                    }
                    //TODO query
                }
                if (this.tsProducts.IsChecked == true) {
                    if (this._productList == null) {
                        this._productList = new List<Product>();
                    }
                    else {
                        this._productList.Clear();
                    }
                    var products = txtProducts.Text.Split(','); // txt de ki product lari , e gore ayirip trimledikten sonra diziye atilmasi
                    foreach (var product in products.Select(t => t.Trim()).Where(product => product.Length > 0)) {
                        this._productList.Add(
                            new Product {
                                            Name = product
                                        });
                    }
                    if (this._productList.Count > 0) {
                        //TODO
                        query += "  ";
                    }
                }
                query += " ORDER BY i.invoice_id;";

                InvoiceSelect iS = new InvoiceSelect(new DbOperations());
                Debug.WriteLine(query);
                dg.ItemsSource = iS.SelectAsTable(query).DefaultView;
            }
            else {
                MessageBox("SEARCH", "PLEASE SELECT A GROUP");
            }
        }


        private async void MessageBox(string title, string message)
        {
            var window = this.TryFindParent<MetroWindow>();
            await window.ShowMessageAsync(title, message);
        }
    }

}