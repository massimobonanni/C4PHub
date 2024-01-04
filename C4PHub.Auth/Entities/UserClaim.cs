namespace C4PHub.Auth.Entities
{
    public class UserClaim
    {
        public UserClaim()
        {

        }

        public UserClaim(System.Security.Claims.Claim sourceClaim)
        {
            this.Issuer = sourceClaim.Issuer;
            this.Value = sourceClaim.Value;
            this.OriginalIssuer = sourceClaim.OriginalIssuer;
            this.ValueType = sourceClaim.ValueType;
            this.Subject = sourceClaim.Subject?.Name;
            this.Type = sourceClaim.Type;
            this.Properties = GenerateStringFromProperties(sourceClaim.Properties);
        }

        public string Issuer { get; set; }
        public string Value { get;  set; }
        public string OriginalIssuer { get;  set; }
        public string ValueType { get;  set; }
        public string Subject { get;  set; }
        public string Type { get;  set; }
        public string Properties { get;  set; }

        private string GenerateStringFromProperties(IDictionary<string, string> properties)
        {
            var result = "";
            foreach (var pair in properties)
            {
                result += $"{pair.Key}={pair.Value} ; ";
            }
            return result.TrimEnd(';', ' ');
        }
    }
}
