using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using MundiPagg.MundiPaggClientModels;
using Newtonsoft.Json;

namespace MundiPagg.Internal
{
    public class MundiPaggHTTP : SingletonMonoBehaviour<MundiPaggHTTP>
    {
        public const string PORTAL_BASE_URL = "https://api-dashboard.mundipagg.com";
        public const string SANDBOX_BASE_URL = "https://sandbox.mundipaggone.com";

        public static Action OnRequestStart, OnRequestEnd;

        public static void MakePortalApiCall<TResult>(Request request, Action<TResult> resultCallback, Action<MundiPaggError> errorCallback)
            where TResult : MundiPaggResultCommon
        {
            RequestContainer requestContainer = new RequestContainer();
            requestContainer.Request = request;
            requestContainer.FullUrl = PORTAL_BASE_URL + request.ApiEndPoint;
            requestContainer.SuccessCallback = () =>
            {
                TResult result = JsonConvert.DeserializeObject<TResult>(requestContainer.JsonResponse);
                if(result as LoginResult != null)
                    MundiPaggSession.CurrentSession = JsonConvert.DeserializeObject<MundiPaggSession.Session>(requestContainer.JsonResponse);
                resultCallback( result );
            };
            requestContainer.ErrorCallback = errorCallback;

            instance.StartCoroutine(instance.StartRequest(requestContainer));
        }
        
        private IEnumerator StartRequest(RequestContainer requestContainer)
        {
            byte[] postData = null;
            if(requestContainer.Request.Verb == Request.VERB.POST)
            {
                string body = JsonConvert.SerializeObject(requestContainer.Request.Data);
                postData = System.Text.Encoding.UTF8.GetBytes(body);
            }
            else
            {

            }

            WWW www = new WWW(requestContainer.FullUrl, postData, requestContainer.Request.Header);

            if(OnRequestStart != null)
                OnRequestStart.Invoke();

            yield return www;

            Debug.Log(www.text);

            //No error?
            if (string.IsNullOrEmpty(www.error))
            {
                requestContainer.JsonResponse = www.text;
                requestContainer.SuccessCallback();
            }
            else
            {
                MundiPaggError error = ConvertError(www.error, www.text);
                //Call error callback
                requestContainer.ErrorCallback(error);
            }

            if(OnRequestEnd != null)
                OnRequestEnd.Invoke();
        }
        private MundiPaggError ConvertError(string wwwError, string wwwJsonMessage)
        {
            MundiPaggError mundiPaggError = new MundiPaggError();

            //Get error message
            try
            {
                mundiPaggError = JsonConvert.DeserializeObject<MundiPaggError>(wwwJsonMessage);
            }
            catch
            {
                mundiPaggError.Errors = new MundiPaggError.Error[]
                {
                    new MundiPaggError.Error()
                    { Message = "Unknown error.", Property = "Unknown property" }
                };
            }

            //Get error code
            try     { mundiPaggError.ErrorCode = (MundiPaggErrorCode)int.Parse(wwwError.Substring(0, 3)); }
            catch   { mundiPaggError.ErrorCode = MundiPaggErrorCode.UNKOWN_ERROR; }
            
            return mundiPaggError;
        }
        
        public class RequestContainer
        {
            public Request Request;
            public string FullUrl;
            public string JsonResponse;
            public Action SuccessCallback;
            public Action<MundiPaggError> ErrorCallback;
        }
        public class Request
        {
            public enum VERB
            {
                GET, POST,
            }
            public VERB Verb;
            public string ApiEndPoint;
            public Dictionary<string, string> Header = new Dictionary<string, string>();
            public MundiPaggRequestCommon Data;
        }   
    }
}