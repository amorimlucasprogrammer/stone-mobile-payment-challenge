using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePopup : SingletonMonoBehaviour<MessagePopup>
{
    [SerializeField]
    private GameObject popupObject;
    [SerializeField]
    private Text title, message;
    
    public static void Open(string title, string message)
    {
        instance.title.text = title;
        instance.message.text = message;

        instance.popupObject.SetActive(true);
    }

    public void Close()
    {
        popupObject.SetActive(false);
    }
}
