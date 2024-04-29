namespace AdsAPI.User
{
    public class UserCredentials
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() // Full read/write access
            {
                UserName = "admin",
                EmailAddress = "admin@email.se",
                Password = "passwordAdmin",
                GivenName = "Gustav",
                SurName = "Ivering",
                Role = "Admin",
            },
            new UserModel() // Can only Read
            {
                UserName = "user",
                EmailAddress = "user@email.se",
                Password = "passwordUser",
                GivenName = "Gustav",
                SurName = "Ivering",
                Role = "User",
            }
        };
    }
}
