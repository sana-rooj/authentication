using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace authentication.Models
{
    public class fbAccessTokenValidation
    {
        [JsonProperty("data")]
        internal FacebookUserAccessTokenData data { get; set; }


    }
    internal class FacebookUserAccessTokenData
    {

        [JsonProperty("app_id")]
        public string AppId { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("expires_at")]
        public long ExpiresAt { get; set; }
        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}
