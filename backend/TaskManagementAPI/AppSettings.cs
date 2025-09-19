namespace TaskManagementAPI
{
    public class AppSettings
    {
        public string JwtKey { get; set; }
        public int ExpireMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
       
    }
}
