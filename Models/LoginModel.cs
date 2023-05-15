namespace Office.Models
{
    /// <summary>
    /// Modelo de Login
    /// </summary>
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// Modelo de Usuário
    /// </summary>
    public class UserModel
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public int FuncId { get; set; }
    }
}
