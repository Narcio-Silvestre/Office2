namespace Office.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserModel
    {
        public string name { get; set; }
        public string id { get; set; }
        public int funcId { get; set; }
    }
}
