namespace Core.Settings
{
    public class JWTConfig
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public JWTConfig Properties { get; set; }

        public JWTConfig() => Properties = this;
    }
}
