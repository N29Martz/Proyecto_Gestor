namespace gestor_archivos_backend.Dtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
