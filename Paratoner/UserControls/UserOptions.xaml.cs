#region

using System.Windows;
using System.Windows.Controls;

using DatabaseClasses;

using FastQueries.Select;
using FastQueries.Update;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

using PsqlConnector;

#endregion

namespace Paratoner.UserControls {

    public partial class UserOptions : UserControl {
        private readonly int _userId;
        private User _user;

        public UserOptions(int userId) {
            this._userId = userId;
            InitializeComponent();
            this.Loaded += UserOptions_Loaded;
        }

        private void UserOptions_Loaded(object sender, RoutedEventArgs e) {
            var vua = new ViewUserAll(new DbOperations());

            this._user = vua.SelectUser(this._userId);

            this.txtUserName.Text = this._user.Info.Name;
            this.txtUserLastName.Text = this._user.Info.LastName;
            this.txtUserEmail.Text = this._user.Info.EMail;
            this.txtUserPhone.Text = this._user.Info.PhoneNumber;
        }

        private void btnSaveInfo_Click(object sender, RoutedEventArgs e) {
            if (this.txtUserLastName.Text.Length > 0
                && this.txtUserEmail.Text.Length > 0
                && this.txtUserName.Text.Length > 0
                && this.txtUserPhone.Text.Length > 0) {
                var uui = new UpdateUserInfo(new DbOperations());

                this._user.Info.Name = this.txtUserName.Text;
                this._user.Info.LastName = this.txtUserLastName.Text;
                this._user.Info.EMail = this.txtUserEmail.Text;
                this._user.Info.PhoneNumber = this.txtUserPhone.Text;

                var result = uui.UpdateUser(this._user);
                MessageBox("SAVE USER INFO", result ? "SUCCESS" : "COULD NOT SAVED");
            }
            else {
                MessageBox("SAVE USER INFO", "FILL EMPTY BLOCKS");
            }
        }

        private void btnSavePassword_Click(object sender, RoutedEventArgs e) {
            var oldpass = txtOldPassword.Password;
            var pass1 = txtNewPassword1.Password;
            var pass2 = txtNewPassword2.Password;

            if (oldpass.Length > 0 && pass1.Length > 0 && pass2.Length > 0) {
                if (pass1 != pass2) {
                    MessageBox("CHANGE PASSWORD", "NEW PASSWORDS NOT EQUAL");
                }
                else {
                    var cp = new CheckPassword(new DbOperations());
                    if (cp.Check(_userId, oldpass)) {
                        var change = new ChangePassword(new DbOperations());
                        if (change.Do(this._userId, pass1)) {
                            MessageBox("CHANGE PASSWORD", "SUCCESS!");

                            txtOldPassword.Password = string.Empty;
                            txtNewPassword2.Password = string.Empty;
                            txtNewPassword1.Password = string.Empty;
                        }
                        else {
                            MessageBox("CHANGE PASSWORD", "Something happend during update to database");
                        }
                    }
                    else {
                        MessageBox("CHANGE PASSWORD", "CURRENT PASSWORD NOT CORRECT!");
                    }
                }
            }
        }

        private async void MessageBox(string title, string message) {
            var window = this.TryFindParent<MetroWindow>();
            await window.ShowMessageAsync(title, message);
        }
    }

}