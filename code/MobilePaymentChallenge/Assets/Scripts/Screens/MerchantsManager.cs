using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MundiPagg;
using MundiPagg.Internal;
using MundiPagg.MundiPaggClientModels;

public class MerchantsManager : Screen
{
    [SerializeField]
    private RectTransform merchantListParent;
    [SerializeField]
    private MerchantElement merchantElementReference;

    [SerializeField]
    private Text paginationNumber;
    [SerializeField]
    private Button backButton, nextButton;

    [SerializeField]
    private CreditCardSalePopup salePopup;

    private GetMerchantsResult merchantsResult;
    private Merchant currentMerchant;

    [SerializeField]
    private MessagePopup errorPopup;

    void Start()
    {
        GetMerchantsPageAndFill(1);
    }

    private void GetMerchantsPageAndFill(int page)
    {
        MerchantElement[] merchantsList = merchantListParent.GetComponentsInChildren<MerchantElement>(false);
        foreach(var m in merchantsList)
        {
            DestroyImmediate(m.gameObject);
        }

        GetMerchantsRequest request = new GetMerchantsRequest()
        {
            CustomerKey = MundiPaggSession.CurrentSession.CustomerKey,
            PageNumber = page,
            PageSize = 10
        };
        MundiPaggClientAPI.GetMerchants(request,
            result =>
            {
                merchantsResult = result;

                SetupMerchantsResult();
                UpdatePaginationView();
            },
            error =>
            {
                MessagePopup.Open(error.ErrorCode.ToString(), error.GetErrorMessage());
            }
        );
    }
    
    private void SetupMerchantsResult()
    {
        for (int i = 0; i < merchantsResult.Merchants.Length; i++)
        {
            int index = i;
            MerchantElement element = CreateMerchantElementAndFill(merchantsResult.Merchants[i]);
            element.OnSaleClick = () =>
            {
                currentMerchant = merchantsResult.Merchants[index];
                OpenAndSetupSalePopup();
            };
        }
    }
    private void UpdatePaginationView()
    {
        if (merchantsResult == null)
            return;

        int pageNumber = merchantsResult.Pagination.options.PageNumber;
        int pageCount = merchantsResult.Pagination.options.PageCount;

        backButton.interactable = (pageNumber > 1);
        nextButton.interactable = (pageNumber < pageCount);

        paginationNumber.text = pageNumber + "/" + pageCount;
    }
    
    private MerchantElement CreateMerchantElementAndFill(Merchant merchant)
    {
        MerchantElement element = GameObject.Instantiate<MerchantElement>(merchantElementReference);
        element.transform.SetParent(merchantListParent);
        element.transform.localScale = Vector3.one;
        element.gameObject.SetActive(true);
        element.SetupView(merchant.MerchantName, merchant.MerchantKey);

        return element;
    }
    
    private void OpenAndSetupSalePopup()
    {
        salePopup.Open();
        salePopup.OnSaleClick =
            creditCardTransaction =>
            {
                SaleRequest request = new SaleRequest()
                {
                    MerchantKey = currentMerchant.MerchantKey,
                    CreditCardTransactionCollection = new CreditCardTransaction[] { creditCardTransaction }
                };
                MundiPaggClientAPI.Sale(request,
                    result =>
                    {
                        salePopup.Close();
                        float transactionCost = creditCardTransaction.AmountInCents / 100f;
                        MessagePopup.Open("Sucesso", string.Format("Compra de R$ {0:0.00} efetuada com sucesso!", transactionCost));
                    },
                    error =>
                    {
                        MessagePopup.Open(error.ErrorCode.ToString(), error.GetErrorMessage());
                    }
                );
            };
    }

    public void LoadBackPage()
    {
        if (merchantsResult == null)
            return;

        int pageNumber = merchantsResult.Pagination.options.PageNumber;
        if (pageNumber <= 1)
            return;
        GetMerchantsPageAndFill(pageNumber - 1);
    }
    public void LoadNextPage()
    {
        if (merchantsResult == null)
            return;

        int pageNumber = merchantsResult.Pagination.options.PageNumber;
        int pageCount = merchantsResult.Pagination.options.PageCount;

        if (pageNumber >= pageCount)
            return;

        GetMerchantsPageAndFill(pageNumber + 1);
    }
}
