#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using DatabaseClasses;

using FastQueries.Delete;
using FastQueries.Select;
using FastQueries.Update;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

using PsqlConnector;

#endregion

namespace Paratoner.UserControls {

    public partial class AdminOperations : UserControl {
        private DbOperations _dbo = new DbOperations();
        private List<GroupsOfMember> _groupList;
        private int _userId;

        public AdminOperations(int userId) {
            this._userId = userId;
            InitializeComponent();
        }

        private void cbxGroupList_OnLoaded(object sender, RoutedEventArgs e) {
            UpdateCombobox();
        }

        private void UpdateCombobox() {
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
            var dt = userGroupList.SelectGroupsOnlyAsAdmin(userId);
            return GroupsOfMember.TableToList(dt);
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            UpdateGrid();
        }

        private void UpdateGrid() {
            if (this.cbxGroupList.SelectedIndex > 0) {
                var ggm = new GetGroupMembers(this._dbo);
                var groupIndex = this.cbxGroupList.SelectedIndex;
                var groupId = this._groupList[groupIndex].GroupId;
                if (this.tgShowAll.IsChecked == true) {
                    var dt = ggm.SelectByGroupId(groupId);
                    this.dg.ItemsSource = dt.DefaultView;
                }
                else {
                    var dt = ggm.SelectOnlyApporedByGroupId(groupId);
                    this.dg.ItemsSource = dt.DefaultView;
                }
            }
            else {
                this.dg.ItemsSource = null;
            }
        }

        private void btnAccepMember_Click(object sender, RoutedEventArgs e) {
            var index = dg.SelectedIndex;
            if (index != -1) {
                var dt = ( (DataView) dg.ItemsSource ).ToTable();
                if (dt.Rows.Count > 0) {
                    var groupmemberId = Convert.ToInt32(dt.Rows[index][1].ToString());
                    var autg = new AcceptUserToGroup(this._dbo);
                    MessageBox("ACCEPT USER", autg.UpdateWithGroupmemberId(groupmemberId) ? "SUCCESS" : "ERROR!");
                    UpdateGrid();
                }
            }
        }

        private void btnDeleteMember_Click(object sender, RoutedEventArgs e) {
            var index = dg.SelectedIndex;
            if (index != -1) {
                var dt = ( (DataView) dg.ItemsSource ).ToTable();
                if (dt.Rows.Count > 0) {
                    var groupmemberId = Convert.ToInt32(dt.Rows[index][1].ToString());
                    var dufgm = new DeleteUserFromGroupmember(this._dbo);
                    MessageBox("DELETE USER", dufgm.DeleteViaGroupmemberId(groupmemberId) ? "SUCCESS" : "ERROR!");
                    UpdateCombobox();
                }
            }
        }

        private async void MessageBox(string title, string message) {
            var window = this.TryFindParent<MetroWindow>();
            await window.ShowMessageAsync(title, message);
        }

        private void ToggleSwitch_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }
    }

    internal class AUser : User {
        public bool IsSelected { get; set; }
    }

}