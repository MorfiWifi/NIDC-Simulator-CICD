namespace Models.Identity
{
    public class PlainToken
    {
        public string AccessToken { get; set; }
        public long ExpirationDate { get; set; }
        public string RefreshToken { get; set; }
    }
}
