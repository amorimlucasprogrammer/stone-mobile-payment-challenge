using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MundiPagg.MundiPaggClientModels
{
    public class MundiPaggRequestCommon
    {
    }

    public class MundiPaggResultCommon
    {
        public MundiPaggRequestCommon Request;
        public object CustomData;
    }

    [Serializable]
    public class LoginRequest : MundiPaggRequestCommon
    {
        public string Username;
        public string Password;
    }

    [Serializable]
    public class LoginResult : MundiPaggResultCommon
    {
        public string UserId;
        public string Name;
        public string Username;
        public string CustomerKey;
    }
}
