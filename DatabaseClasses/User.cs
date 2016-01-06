namespace DatabaseClasses
{
    public class User
    {
        public int UserId{ get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LastLoginTime { get; set; }
        public UserInfo Info { get; set; }
    }
}
