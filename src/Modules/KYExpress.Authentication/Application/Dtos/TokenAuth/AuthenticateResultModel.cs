namespace KYExpress.Authentication.Application.Dtos.TokenAuth
{
    public class AuthenticateResultModel
    {
        public string AccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public string UserId { get; set; }
    }
}
