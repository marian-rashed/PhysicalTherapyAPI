namespace PhysicalTherapyAPI.DTOs
{
    public class ResetPasswordDTO
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
