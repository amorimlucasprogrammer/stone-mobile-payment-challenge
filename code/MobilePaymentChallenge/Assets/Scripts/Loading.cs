using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MundiPagg.Internal;

public class Loading : SingletonMonoBehaviour<Loading>
{
    [SerializeField]
    private GameObject loadingObject;

    void Start()
    {
        MundiPaggHTTP.OnRequestStart +=
            () =>
            {
                loadingObject.SetActive(true);
            };
        MundiPaggHTTP.OnRequestEnd +=
            () =>
            {
                loadingObject.SetActive(false);
            };
    }
}
