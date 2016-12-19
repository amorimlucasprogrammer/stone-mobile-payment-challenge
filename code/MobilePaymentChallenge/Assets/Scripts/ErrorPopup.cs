using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPopup : MonoBehaviour
{
    [SerializeField]
    private GameObject popupObject;
    [SerializeField]
    private Text errorTitle, errorMessage;

    public void Open(string title, string message)
    {
        errorTitle.text = title;
        errorMessage.text = message;

        popupObject.SetActive(true);
    }

    public void Close()
    {
        popupObject.SetActive(false);
    }
}
