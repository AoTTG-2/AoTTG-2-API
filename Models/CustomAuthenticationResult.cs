using System.Collections.Generic;

namespace AoTTG2.IDS.Models
{
    public class CustomAuthenticationResult
    {
        public PhotonResultCode ResultCode { get; set; }

        public string Message { get; set; }

        public string UserId { get; set; }

        public string Nickname { get; set; }

        public Dictionary<string, object> Data { get; set; }

        public Dictionary<string, object> AuthCookie { get; set; }

        public long? ExpireAt { get; set; }
    }
}
