﻿#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using DatabaseClasses;

using FastQueries.Insert;
using FastQueries.Select;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

using Paratoner.UserControls;

using PsqlConnector;

#endregion

namespace Paratoner {

    public partial class MainWindow {
        private readonly int _userId;
        private readonly DbOperations _dbo = new DbOperations();
        private List<GroupsOfMember> _groupList;
        private List<Product> _productList;

        private readonly GroupOperations      _groupOperations;
        private readonly UserOptions          _userOptions;
        private readonly InvoiceLoanCalculate _invoiceLoanCalculate;
        private readonly AdminOperations      _adminOperations;
        private readonly InvoiceOperations    _invoiceOperations;

        public MainWindow(int userId) {
            this._userId               = userId;
            this._userOptions          = new UserOptions(userId);
            this._invoiceOperations    = new InvoiceOperations(userId);
            this._invoiceLoanCalculate = new InvoiceLoanCalculate(userId);
            this._adminOperations      = new AdminOperations(userId);
            this._groupOperations      = new GroupOperations(userId);
            
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            this.tabiInvoiceOperations.Content = this._invoiceOperations;
            this.tabInvoiceLoanCalculate.Content = this._invoiceLoanCalculate;
        }

        #region EVENTS

        private void BtnDeleteProduct_OnClick(object sender, RoutedEventArgs e) {
            var parent = ( (Button) sender ).TryFindParent<Grid>();
            int  selectedIndex = Convert.ToInt32(( (TextBlock) parent.Children[0] ).Text);
            var deleteIndex = selectedIndex - 1;
            this._productList.RemoveAt(deleteIndex);
            //git parenti bul oradan index txt ye gec, numarayi al gel bana, sonra onu sil
            //sildikten sonra silinen indexten baslayarak(o silindigi icin yerine bir sonraki geldi, yarista ikinciyi gecersen kacinci olursun sorusuyla ayni cevap
            //ondan sonraki indexleri 1 azalt cunku sira degisti

            for (int i = deleteIndex; i < this._productList.Count; i++) {
                --this._productList[i].ProductId;
            }

            this.lbxProductList.ItemsSource = null;
            this.lbxProductList.ItemsSource = this._productList;
        }

        private void cbxGroupList_OnLoaded(object sender, RoutedEventArgs e) {
            LoadGroupList();
        }

        private void btnAddProduct_OnClick(object sender, RoutedEventArgs e) {
            if ( this.txtProduct.Text.Length > 0) {
                AddProduct();
            }
            this.txtProduct.Focus();
        }

        private void txtProduct_OnKeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                AddProduct();
            }
        }

        private async void btnAddInvoice_OnClick(object sender, RoutedEventArgs e) {
            if ( this.cbxGroupList.SelectedIndex < 1) {
                await this.ShowMessageAsync("SAVE INVOICE", "Please select a group!");
                return;
            }
            if ( this.txtPrice.Text == String.Empty) {
                await this.ShowMessageAsync("SAVE INVOICE", "Price cannot be empty!");
                return;
            }
            double price;
            if (!double.TryParse(this.txtPrice.Text, out price)) {
                await this.ShowMessageAsync("SAVE INVOICE", "Enter a valid price!");
                return;
            }

            var groupId = this._groupList[this.cbxGroupList.SelectedIndex].GroupId;
            var userId = this._userId;
            var time = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            var invoice = new Invoice {
                                          GroupId = groupId,
                                          UserId = userId,
                                          BuyDate = time,
                                          IsExpired = false,
                                          Price = price
                                      };

            var add = new AddNewInvoice(this._dbo);
            if (add.Insert(invoice)) {
                await this.ShowMessageAsync("SAVE INVOICE", "Inovice Saved!");

                var invoiceId = add.GetLastInvoiceId();

                if (this._productList.Count > 0) {
                    // eger product eklenmisse kaydet
                    var saveProduct = new AddNewProduct(this._dbo);

                    if (saveProduct.Insert(this._productList, invoiceId)) {
                        await this.ShowMessageAsync("SAVE PRODUCTS", "Products Saved!");

                        ClearInvoiceScreen();
                    }
                    else {
                        await this.ShowMessageAsync("SAVE PRODUCTS", "Something gone wrong!");
                    }
                }
            }
            else {
                await this.ShowMessageAsync("SAVE INVOICE", "Something gone wrong!");
            }
        }

        private void btnSettings_OnClick(object sender, RoutedEventArgs e) {
            this.grdInvoice.Visibility = Visibility.Hidden;

            this.grdOptions.Children.Clear();
            this.grdOptions.Children.Add(this._userOptions);

            this.grdOptions.Visibility = Visibility.Visible;
        }


        private void btnInvoiceOperations_OnClick(object sender, RoutedEventArgs e)
        {
            this.grdOptions.Visibility = Visibility.Hidden;
            this.grdInvoice.Visibility = Visibility.Visible;

        }

        private void btnGroupOperations_OnClick(object sender, RoutedEventArgs e)
        {
            this.grdInvoice.Visibility = Visibility.Hidden;
            this.grdOptions.Children.Clear();
            this.grdOptions.Children.Add(this._groupOperations);
            this.grdOptions.Visibility = Visibility.Visible;
        }

        private void btnUserList_OnClick(object sender, RoutedEventArgs e)
        {
            this.grdInvoice.Visibility = Visibility.Hidden;
            this.grdOptions.Children.Clear();
            this.grdOptions.Children.Add(this._adminOperations);
            this.grdOptions.Visibility = Visibility.Visible;
        }

        private void tabInvoice_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (this.tabInvoice.SelectedIndex == 1) {
                if (this.WindowState != WindowState.Maximized) {
                    this.Height = 680;
                }
            }
            if (this.tabInvoice.SelectedIndex == 2) {
                

            }
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
            var userGroupList = new SelectUserGroupList(this._dbo);
            return GroupsOfMember.TableToList(userGroupList.SelectGroups(userId));
        }

        private void AddProduct() {
            if (this._productList == null) {
                this._productList = new List<Product>();
            }
            var productName = this.txtProduct.Text.Trim();
            this._productList.Add(
                new Product {
                                ProductId = this._productList.Count + 1,
                                Name = productName
                            });
            this.txtProduct.Text = string.Empty;
            this.lbxProductList.ItemsSource = null;
            this.lbxProductList.ItemsSource = this._productList;
        }

        private void ClearInvoiceScreen() {
            this.lbxProductList.ItemsSource = null;
            this.lbxProductList.Items.Clear();
            this.txtProduct.Text = string.Empty;
            this.txtPrice.Text = string.Empty;
            this._productList.Clear();
        }

        #endregion

        private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e) {
            ( (TextBlock) sender ).TryFindParent<Grid>().TryFindParent<ListBoxItem>().IsSelected = true;
        }
    }

}