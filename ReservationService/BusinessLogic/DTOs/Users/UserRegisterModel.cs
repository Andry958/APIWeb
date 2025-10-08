namespace BusinessLogic.DTOs.Users
{
    public class UserRegisterModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}
