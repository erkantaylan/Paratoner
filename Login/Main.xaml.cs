#region

using System;
using System.Globalization;
using System.Windows;

using FastQueries;
using FastQueries.Select;
using FastQueries.Update;

using MahApps.Metro.Controls.Dialogs;

using PsqlConnector;

#endregion

namespace ET.Login {

    public partial class Main {
        private int UserId { get; set; }
        readonly static DbOperations dbo = new DbOperations();

        public Main() {
            InitializeComponent();


        }

        #region FUNCTIONS

        private static int CheckAccount(string username, string password) {
            var accountController = new AccountController(dbo);
            return accountController.Check(username, password);
        }

        private static bool UpdateLoginTime(int id, string time) {
            var updateTime = new UpdateUserLoginTime(dbo);
            return updateTime.UpdateTime(id, time);
        }

        #endregion

        #region EVENTS

        private async void btnLogin_OnClick(object sender, RoutedEventArgs e) {
            progressRing.IsActive = true;
            bool openParatoner = false;
            var username = txtUsername.Text;
            var password = txtPassword.Password;
            var id = CheckAccount(username, password); // username password dogru mu
            if (id != -1) {
                await this.ShowMessageAsync("Login", "Succesfull");
                this.UserId = id;
                var isUpdated = UpdateLoginTime(id, DateTime.Now.ToString(CultureInfo.InvariantCulture)); // kullanicinin ne zaman login oldugunu update et
                if (isUpdated) {
                    openParatoner = true;
                }
            }
            else {
                await this.ShowMessageAsync("Login", $"Fail id:{id}");
            }
            progressRing.IsActive = false;
            if (openParatoner) {
                var paratoner = new Paratoner.MainWindow(UserId);
                paratoner.Show();
                this.Close();
            }
        }

        private async void btnForgotPassword_OnClick(object sender, RoutedEventArgs e) {
            await this.ShowMessageAsync("Forgot Password", "We're sory for you but nothing we could do for you now.");
        }

        #endregion
    }

}