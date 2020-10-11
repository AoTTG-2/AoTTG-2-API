namespace AoTTG2.IDS.Models
{
    public enum PhotonResultCode : byte
    {
        /// <summary>
        /// Authentication incomplete, only Data returned
        /// </summary>
        Incomplete = 0,
        /// <summary>
        /// Authentication successful. UserId, Nickname, AuthCookie and Data can be returned
        /// </summary>
        Success = 1,
        /// <summary>
        /// Authentication failed. Wrong credentials.
        /// </summary>
        Failed = 2,
        /// <summary>
        /// Invalid parameters
        /// </summary>
        Invalid = 3
    }
}
