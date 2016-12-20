using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MundiPagg.MundiPaggClientModels;

public class CreditCardSalePopup : MonoBehaviour
{
    [SerializeField]
    private GameObject popupObject;

    [SerializeField]
    private InputField nameField, emailField, brandField, cardNumberField, cardNameField, securityCodeField, expMonthField, expYearField;

    [SerializeField]
    private InputField costField;

    public Action<CreditCardTransaction> OnSaleClick;

    public void SaleClick()
    {
        float cost = 0;
        float.TryParse(costField.text, out cost);
        int costInCents = Mathf.RoundToInt(cost * 100);

        CreditCard creditCard = new CreditCard()
        {
            HolderName = cardNameField.text,
            CreditCardBrand = brandField.text,
            CreditCardNumber = cardNumberField.text.Replace("-", ""),
        };
        int.TryParse(securityCodeField.text, out creditCard.SecurityCode);
        int.TryParse(expMonthField.text, out creditCard.ExpMonth);
        int.TryParse(expYearField.text, out creditCard.ExpYear);

        CreditCardTransaction transaction = new CreditCardTransaction()
        {
            AmountInCents = costInCents,
            CreditCard = creditCard
        };

        if (OnSaleClick != null)
            OnSaleClick(transaction);
    }

    public void Open()
    {
        popupObject.SetActive(true);
    }
    public void Close()
    {
        popupObject.SetActive(false);
    }
}
