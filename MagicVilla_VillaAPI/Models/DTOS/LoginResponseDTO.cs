namespace MagicVilla_VillaAPI.Models.DTOS
{
    public class LoginResponseDTO
    {
        public LocalUser User { get; set; }
        public string Token { get; set; }
    }
}
