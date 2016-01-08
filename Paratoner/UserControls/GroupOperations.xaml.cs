using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DatabaseClasses;

using FastQueries.Insert;
using FastQueries.Select;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

using PsqlConnector;

namespace Paratoner.UserControls {

    public partial class GroupOperations : UserControl {

        private readonly int _userId;
        private List< Group > _groupList;

        public GroupOperations(int userId) {
            this._userId = userId;
            InitializeComponent();
        }

        private void dg_Loaded(object sender, RoutedEventArgs e) {
            Load_Datagrid();
        }

        private void Load_Datagrid() {
            var gwu = new GroupsWithoutUser(new DbOperations());
            var dt = gwu.GetAsTable(this._userId);
            this._groupList = gwu.TableToList(dt);
            this.dg.ItemsSource = dt.DefaultView;
        }

        private void btnJoin_OnClick(object sender, RoutedEventArgs e) {
            var index = this.dg.SelectedIndex;
            if ( index > -1 ) {
                var groupId = this._groupList[index].GroupId;
                var join = new JoinGroup(new DbOperations());
                if ( join.Join(this._userId, groupId) ) {
                    MessageBox("JOIN", "SUCCESS");
                    Load_Datagrid();
                } else {
                    MessageBox("JOIN", "SOME ERROR");
                }
            }
        }

        private async void MessageBox(string title, string message) {
            var window = this.TryFindParent< MetroWindow >();
            await window.ShowMessageAsync(title, message);
        }

        private void btnCreateGroup_Click(object sender, RoutedEventArgs e) {
            var groupName = this.txtHomeName.Text;
            if ( groupName.Length > 0 ) {
                var value = this.nudCutOffDay.Value;

                var he = new HomeExist(new DbOperations());
                if ( !he.CheckName(groupName) ) {
                    var cutoffday = 1;
                    if ( value != null ) {
                        cutoffday = (int) value;
                    }
                    var adg = new AddNewGroup(new DbOperations());
                    int groupId = he.GetGroupId(groupName);
                    var result = adg.Add(
                        new Group {
                            Name = groupName,
                            CutOffDate = cutoffday,
                            GroupId = groupId
                        });
                    MessageBox("CREATE GROUP", result ? "SUCCESS" : "ERROR:CREATING GROUP");
                } else {
                    MessageBox("CREATE GROUP", $"{groupName} IS ALREADY IN USE!");
                }
            }
        }

    }

}