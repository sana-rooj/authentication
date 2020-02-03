using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace authentication.Models
{
    public class UserCredentails
    {
       
            public long Id { get; set; }
            public string Email { get; set; }
            public string Name { get; set; }
            [JsonProperty("first_name")]
            public string FirstName { get; set; }
            [JsonProperty("last_name")]
            public string LastName { get; set; }
            public string Gender { get; set; }
            public string Locale { get; set; }
            public FacebookPictureData Picture { get; set; }

        public class FacebookPictureData
        {
            public FacebookPicture Data { get; set; }
        }

        public class FacebookPicture
        {
            public int Height { get; set; }
            public int Width { get; set; }
            [JsonProperty("is_silhouette")]
            public bool IsSilhouette { get; set; }
            public string Url { get; set; }
        }
    }
}
