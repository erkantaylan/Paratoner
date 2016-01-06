#region

using System.Diagnostics;

using DatabaseClasses;

using PsqlConnector;

#endregion

namespace FastQueries.Update {

    public class UpdateUserInfo {
        private DbOperations Dbo { get; set; }

        public UpdateUserInfo(DbOperations dbo) {
            this.Dbo = dbo;
        }

        public bool UpdateUser(User user) {
            if (user?.Info == null) {
                return false;
            }
            if (user.Info.UserId <= 0) {
                return false;
            }
            /*
UPDATE public.userinfo
   SET userinfo_id=?, name=?, lastname=?, phone=?, email=?, user_id=?
 WHERE <condition>;
            
            */
            var query = "UPDATE public.userinfo " +
                           "SET " +
                           $"name='{user.Info.Name}'" +
                           $",lastname='{user.Info.LastName}'" +
                           $",phone='{user.Info.PhoneNumber}'" +
                           $",email='{user.Info.EMail}' " +
                           $"WHERE user_id = {user.Info.UserId}";
            var rowCount = Dbo.RunCommand(query);
            if (rowCount > 0) {
                return true;
            }
            Debug.WriteLine("UpdateUesrInfo.UpdateUser");
            Debug.WriteLine($"Error:{query}");
            return false;
        }

    }

}