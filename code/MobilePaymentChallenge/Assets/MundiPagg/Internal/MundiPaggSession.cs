using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MundiPagg.Internal
{
    public class MundiPaggSession
    {
        public static Session CurrentSession;

        [System.Serializable]
        public class Session
        {
            public string AccessToken;
            public string TokenType;
            public int ExpiresIn;
            public string RefreshToken;
            public string Name;
            public string Username;
            public string CustomerKey;
        }
    }
}