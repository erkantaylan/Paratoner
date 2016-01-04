using System;

using PsqlConnector;

namespace FastQueries.Update
{
    public class UpdateUserLoginTime
    {
        private DbOperations Dbo { get; set; }

        public UpdateUserLoginTime(DbOperations dbo) {
            this.Dbo = dbo;
        }


        /*
        UPDATE public."user"
        SET last_login_time=?
        WHERE <condition>;
        */
        public bool UpdateTime(int userId, string time) {
            var query = "UPDATE public.\"user\"" +
                        $" SET last_login_time=\'{time}\'" +
                        $" WHERE \"user\".user_id={userId}";
            var rowCount = Dbo.RunCommand(query);
            return rowCount > 0;
        }
    }
}
