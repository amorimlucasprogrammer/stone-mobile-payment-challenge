using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;

namespace MundiPagg.MundiPaggClientModels
{
    public class MundiPaggRequestCommon
    {
    }

    public class MundiPaggResultCommon
    {
        public MundiPaggRequestCommon Request;
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

    [Serializable]
    public class GetMerchantsRequest : MundiPaggRequestCommon
    {
        [JsonIgnore]
        public string CustomerKey;
        public string Merchant;
        public int PageNumber;
        public int PageSize;
    }
    [Serializable]
    public class GetMerchantsResult : MundiPaggResultCommon
    {
        public MerchantPagination Pagination;

        [JsonProperty("items")]
        public Merchant[] Merchants;
    }

    [Serializable]
    public class MerchantPagination
    {
        public Option options;
        [Serializable]
        public class Option
        {
            public int PageNumber;
            public int PageCount;
            public int PageSize;
            public int ItemCount;
        }
    }
    [Serializable]
    public class Merchant
    {
        public string MerchantKey;
        public string PublicMerchantKey;
        public string MerchantName;
        public string DocumentNumber;
        public string ComporateName;
        public bool IsRetryEnabled;
        public bool IsEnabled;
        public bool IsDeleted;
        public bool IsAntiFraudEnabled;
        public string MerchantStatus;
    }
    
    [Serializable]
    public class SaleRequest : MundiPaggRequestCommon
    {
        [JsonIgnore]
        public string MerchantKey;
        public CreditCardTransaction[] CreditCardTransactionCollection;
    }
    [Serializable]
    public class SaleResult : MundiPaggResultCommon
    {
        public CreditCardTransactionResult[] CreditCardTransactionResultCollection;
    }

    public class CreditCardTransactionResult
    {
        public string AcquirerMessage;
        public bool Success;
    }
    public class CreditCardTransaction
    {
        public int AmountInCents;
        public CreditCard CreditCard;
    }
    public class CreditCard
    {
        public string CreditCardBrand;
        public string CreditCardNumber;
        public int ExpMonth;
        public int ExpYear;
        public int SecurityCode;
        public string HolderName;
    }
}
