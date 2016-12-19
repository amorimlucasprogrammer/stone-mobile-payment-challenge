using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MundiPagg.Internal;
using MundiPagg.MundiPaggClientModels;

namespace MundiPagg
{
    public static class MundiPaggClientAPI
    {
        public static bool IsClientLoggedIn()
        {
            return (MundiPaggSession.CurrentSession != null);
        }
        public static void Login(LoginRequest request, Action<LoginResult> resultCallback, Action<MundiPaggError> errorCallback)
        {
            MundiPaggHTTP.Request loginRequest = new MundiPaggHTTP.Request()
            {
                ApiEndPoint = "/users/accesstokens",
                Verb = MundiPaggHTTP.Request.VERB.POST,
                Header = new Dictionary<string, string>()
                {
                    { "Content-Type", "application/json" }
                },
                Data = request
            };
            MundiPaggHTTP.MakePortalApiCall(loginRequest, resultCallback, errorCallback);
        }

        public static void GetMerchants()
        {
            //MundiPaggHTTP.MakePortalApiCall();
        }

        public static void Sale()
        {
           // MundiPaggHTTP.MakePortalApiCall();
        }
    }
}