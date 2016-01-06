#region

using DatabaseClasses;

using PsqlConnector;

#endregion

namespace FastQueries.Select {

    /*
CREATE OR REPLACE VIEW public.userallinfo AS 
 SELECT userinfo.user_id,
    userinfo.name,
    userinfo.lastname,
    "user".username,
    "user".password,
    "user".last_login_time,
    userinfo.email,
    userinfo.phone
   FROM "user",
    userinfo
  WHERE "user".user_id = userinfo.user_id;
    
        */

    public class ViewUserAll {
        private DbOperations Dbo { get; }

        public ViewUserAll(DbOperations dbo) {
            this.Dbo = dbo;
        }

        public User SelectUser(int userId) {
            var user = new User();
            var query = "SELECT * FROM userallinfo " +
                        $"WHERE user_id={userId};";
            var dt = Dbo.SelectTable(query);
            if (dt == null) {
                return null;
            }
            if (dt.Rows.Count == 0) {
                return user;
            }

            var name          = dt.Rows[0][1].ToString();
            var lastname      = dt.Rows[0][2].ToString();
            var username      = dt.Rows[0][3].ToString();
            var password      = dt.Rows[0][4].ToString();
            var lastLogintime = dt.Rows[0][5].ToString();
            var email         = dt.Rows[0][6].ToString();
            var phone         = dt.Rows[0][7].ToString();

            user.UserId = userId;
            user.Info = new UserInfo {
                                         Name = name,
                                         LastName = lastname,
                                         UserId = userId,
                                         EMail = email,
                                         PhoneNumber = phone
                                     };
            user.Username = username;
            user.Password = password;
            user.LastLoginTime = lastLogintime;
            return user;
        }
    }

}