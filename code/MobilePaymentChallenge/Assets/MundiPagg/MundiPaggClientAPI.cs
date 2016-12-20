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
                Data = request,
                Header = new Dictionary<string, string>()
                {
                    { "Content-Type", "application/json" }
                }
            };
            MundiPaggHTTP.MakePortalApiCall(loginRequest, resultCallback, errorCallback);
        }

        public static void GetMerchants(GetMerchantsRequest request, Action<GetMerchantsResult> resultCallback, Action<MundiPaggError> errorCallback)
        {
            MundiPaggHTTP.Request searchMerchantsRequest = new MundiPaggHTTP.Request()
            {
                ApiEndPoint = "/" + request.CustomerKey + "/merchants",
                Verb = MundiPaggHTTP.Request.VERB.GET,
                Data = request,
                Header = new Dictionary<string, string>()
                {
                    { "Authorization", MundiPaggSession.CurrentSession.TokenType + " " + MundiPaggSession.CurrentSession.AccessToken }
                    , { "Content-Type", "application/x-www-form-urlencoded" }
                    , { "IsSandboxEnabled", "true" }
                }
            };
            MundiPaggHTTP.MakePortalApiCall(searchMerchantsRequest, resultCallback, errorCallback);
        }

        public static void Sale(SaleRequest request, Action<SaleResult> resultCallback, Action<MundiPaggError> errorCallback)
        {
            MundiPaggHTTP.Request saleRequest = new MundiPaggHTTP.Request()
            {
                ApiEndPoint = "/Sale",
                Verb = MundiPaggHTTP.Request.VERB.POST,
                Data = request,
                Header = new Dictionary<string, string>()
                {
                    { "MerchantKey", request.MerchantKey }
                    ,{ "Content-Type", "application/json" }
                    , { "Accept", "application/json" }
                    , { "IsSandboxEnabled", "true" }
                    
                }
            };
            MundiPaggHTTP.MakeSandboxApiCall(saleRequest, resultCallback, errorCallback);
        }
    }
}