using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MundiPagg;
using MundiPagg.MundiPaggClientModels;

public class LoginManager : Screen
{
    [SerializeField]
    private string nextScreenId;

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
                usernameField.text = string.Empty;
                passwordField.text = string.Empty;

                ScreenManager.instance.ChangeScreen(nextScreenId);
            },
            error =>
            {
                
            }
        );
    }
}
