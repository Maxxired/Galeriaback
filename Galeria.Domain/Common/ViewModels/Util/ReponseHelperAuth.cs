namespace Galeria.Domain.Common.ViewModels.Util
{
    public class ResponseHelperAuth
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public User User { get; set; } = new User();
    }
}
