using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MundiPagg;
using MundiPagg.MundiPaggClientModels;

public class LoginManager : MonoBehaviour
{
    [SerializeField]
    private InputField usernameField, passwordField;
    
    public void StartLogin()
    {
        string username = usernameField.text;
        string password = passwordField.text;

        LoginRequest request = new LoginRequest()
        {
            Username = username, Password = password
        };

        MundiPaggClientAPI.Login(request,
            result =>
            {
                Debug.Log(result.Username);
                Debug.Log(result.CustomerKey);
            },
            error =>
            {
                Debug.Log(error.ToString());
            }
        );
    }
}
