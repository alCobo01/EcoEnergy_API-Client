namespace T1_PR2_API.DTOs
{
    public class RegisterDTO
    {
        public string Name { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        
    }
}
