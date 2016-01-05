﻿#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using DatabaseClasses;

using FastQueries.Insert;
using FastQueries.Select;

using MahApps.Metro.Controls.Dialogs;

using PsqlConnector;

#endregion

namespace Paratoner {

    public partial class MainWindow {
        private readonly int _userId;
        private readonly DbOperations Dbo = new DbOperations();
        private List<GroupsOfMember> _groupList;
        private List<Product> _productList;

        public MainWindow(int userId) {
            this._userId = userId;
            InitializeComponent();
        }

        #region EVENTS

        private void cbxGroupList_OnLoaded(object sender, RoutedEventArgs e) {
            this.LoadGroupList();
        }

        private void btnAddProduct_OnClick(object sender, RoutedEventArgs e) {
            if (txtProduct.Text.Length > 0) {
                AddProduct();
            }
            txtProduct.Focus();
        }

        private void txtProduct_OnKeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                this.AddProduct();
            }
        }

        private async void btnAddInvoice_OnClick(object sender, RoutedEventArgs e) {
            if (cbxGroupList.SelectedIndex < 1) {
                await this.ShowMessageAsync("SAVE INVOICE", "Please select a group!");
                return;
            }
            if (txtPrice.Text == String.Empty) {
                await this.ShowMessageAsync("SAVE INVOICE", "Price cannot be empty!");
                return;
            }
            double price;
            if (!double.TryParse(txtPrice.Text, out price)) {
                await this.ShowMessageAsync("SAVE INVOICE", "Enter a valid price!");
                return;
            }
            

            int groupId = this._groupList[this.cbxGroupList.SelectedIndex].GroupId;
            int userId = this._userId;
            string time = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            Invoice invoice = new Invoice {
                                              GroupId = groupId,
                                              UserId = userId,
                                              BuyDate = time,
                                              IsExpired = false,
                                              Price = price
                                          };
            
            var add = new AddNewInvoice(Dbo);
            int invoiceId = -1;
            if (add.Insert(invoice)) {
                await this.ShowMessageAsync("SAVE INVOICE", "Inovice Saved!");
                invoiceId = add.GetLastInvoiceId();
                ClearInvoiceScreen();
            }
            else {
                await this.ShowMessageAsync("SAVE INVOICE", "Something gone wrong!");
            }
            if (this._productList.Count > 0) {
                // eger product eklenmisse kaydet
                var saveProduct = new AddNewProduct(Dbo);
                //TODO invoiceId lazim
                if (saveProduct.Insert(this._productList, invoiceId)) {
                    await this.ShowMessageAsync("SAVE PRODUCTS", "Products Saved!");
                }
                else {
                    await this.ShowMessageAsync("SAVE PRODUCTS", "Something gone wrong!");
                }
            }
        }

        private async void btnSettings_OnClick(object sender, RoutedEventArgs e) {
            
        }

        #endregion

        #region FUNCTIONS

        /// <summary>
        ///     This function loads combobox with GroupList via userId
        /// </summary>
        private void LoadGroupList() {
            this._groupList = SelectGroupsOfMemberById(this._userId);
            this._groupList.Insert(
                0,
                new GroupsOfMember {
                                       Name = "Select a group...",
                                       ForegroundBrush = Brushes.Black
                                   });

            this.cbxGroupList.ItemsSource = this._groupList;
            this.cbxGroupList.SelectedIndex = 0;
        }

        /*  "group".name, 
            groupmember.is_admin, 
            groupmember.user_id, 
            groupmember.group_id, 
            groupmember.is_approved, 
            "group".cutoff_day        
        */
        private List<GroupsOfMember> SelectGroupsOfMemberById(int userId) {
            var userGroupList = new SelectUserGroupList(Dbo);
            return GroupsOfMember.TableToList(userGroupList.SelectGroups(userId));
        }

        private void AddProduct() {
            if (this._productList == null) {
                this._productList = new List<Product>();
            }
            var productName = txtProduct.Text.Trim();
            this._productList.Add(
                new Product {
                                Name = productName
                            });
            this.txtProduct.Text = String.Empty;
            this.lbxProductList.ItemsSource = null;
            this.lbxProductList.ItemsSource = this._productList;
        }

        private void ClearInvoiceScreen() {
            this.lbxProductList.ItemsSource = null;
            this.lbxProductList.Items.Clear();
            this.txtProduct.Text = String.Empty;
            this.txtPrice.Text = String.Empty;
        }
        
        #endregion
    }

}