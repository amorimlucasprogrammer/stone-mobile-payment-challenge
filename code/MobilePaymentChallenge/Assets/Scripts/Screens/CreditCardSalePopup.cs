using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MundiPagg.MundiPaggClientModels;

public class CreditCardSalePopup : MonoBehaviour
{
    [SerializeField]
    private InputField nameField, emailField, brandField, cardNumberField, cardNameField, securityCodeField, expMonthField, expYearField;

    [SerializeField]
    private InputField costField;

    public Action<CreditCardTransaction> OnSaleClick;

    public void SaleClick()
    {
        int costInCents = Mathf.RoundToInt( float.Parse(costField.text) * 100 );

        CreditCard creditCard = new CreditCard()
        {
            HolderName = cardNameField.text,
            CreditCardBrand = brandField.text,
            CreditCardNumber = cardNumberField.text,
            SecurityCode = int.Parse(securityCodeField.text),
            ExpMonth = int.Parse(expMonthField.text),
            ExpYear = int.Parse(expYearField.text)
        };
        CreditCardTransaction transaction = new CreditCardTransaction()
        {
            AmountInCents = costInCents,
            CreditCard = creditCard
        };
        
        if (OnSaleClick != null)
            OnSaleClick(transaction);
    }
}
