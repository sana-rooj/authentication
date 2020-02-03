using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace authentication.Models
{
    public class authorizationTokenResource
    {
        public fbTokenResource AccessToken { get; set; }
        public fbTokenResource RefreshToken { get; set; }
    }
}
