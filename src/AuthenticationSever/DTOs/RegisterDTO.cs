namespace AuthenticationSever.DTOs
{
    public class RegisterDTO
    {
        public string FullName { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
        public string Password { set; get; }
        public string Role { set; get; }
    }
}
