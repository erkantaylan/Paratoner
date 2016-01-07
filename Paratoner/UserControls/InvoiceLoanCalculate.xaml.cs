#region

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using DatabaseClasses;

using FastQueries.Select;
using FastQueries.Update;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

using PsqlConnector;

#endregion

namespace Paratoner.UserControls {

    public partial class InvoiceLoanCalculate : UserControl {
        private readonly int _userId;
        private List<GroupsOfMember> _groupList;
        private DbOperations _dbo = new DbOperations();

        public InvoiceLoanCalculate(int userId) {
            this._userId = userId;
            InitializeComponent();
        }

        private void cbxGroupList_OnLoaded(object sender, RoutedEventArgs e) {
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

        private List<GroupsOfMember> SelectGroupsOfMemberById(int userId) {
            var userGroupList = new SelectUserGroupList(this._dbo);
            return GroupsOfMember.TableToList(userGroupList.SelectGroups(userId));
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cbxGroupList.SelectedIndex > 0) {
                //butun faturalar
                InvoiceSelect iS = new InvoiceSelect(this._dbo);
                int groupIndex = cbxGroupList.SelectedIndex;
                int groupId = this._groupList[groupIndex].GroupId;
                var dt = iS.SelectAsTable(groupId);
                gdInvoices.ItemsSource = dt.DefaultView;

                //borclar
                if (dt.Rows.Count > 1) {
                    SpLoanList sp = new SpLoanList(this._dbo);
                    dt = sp.SelectByGroupId(groupId);
                    dgLoan.ItemsSource = dt.DefaultView;
                }
                else {
                    dgLoan.ItemsSource = null;
                }
            }
            else {
                gdInvoices.ItemsSource = null;
                dgLoan.ItemsSource = null;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
            int groupIndex = cbxGroupList.SelectedIndex;
            int groupId = this._groupList[groupIndex].GroupId;

            if (Equals(this._groupList[groupIndex].ForegroundBrush, Brushes.Red)) {
                CutInvoices cut = new CutInvoices(this._dbo);
                bool result = cut.CutByGroupId(groupId);
                this.MessageBox("CUT", result ? "SUCCESS" : "SOMETHING IS WRONG");
            }
            else {
                this.MessageBox("CUT", "YOU ARE NOT ADMIN OF THIS GROUP");
            }
        }

        private async void MessageBox(string title, string message)
        {
            var window = this.TryFindParent<MetroWindow>();
            await window.ShowMessageAsync(title, message);
        }
    }

}