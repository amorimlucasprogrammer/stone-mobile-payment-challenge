using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantElement : MonoBehaviour
{
    [SerializeField]
    private Text merchantName, merchantKey;

    public Action OnSaleClick;

    public void SetupView(string merchantName, string merchantKey)
    {
        this.merchantName.text = merchantName;
        this.merchantKey.text = merchantKey;
    }

    public void SaleClick()
    {
        if (OnSaleClick != null)
            OnSaleClick.Invoke();
    }
}
