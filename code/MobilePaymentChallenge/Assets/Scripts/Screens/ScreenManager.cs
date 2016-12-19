using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MundiPagg.Internal;

public class ScreenManager : SingletonMonoBehaviour<ScreenManager>
{
    [SerializeField]
    private string startScreenId;
    [SerializeField]
    private ScreenStruct[] screens;

    private Dictionary<string, ScreenStruct> screenAccess = new Dictionary<string, ScreenStruct>();
    private string currentScreenId;
    private ScreenStruct currentScreen = null;

	void Start()
    {
        foreach(ScreenStruct screen in screens)
        {
            screenAccess.Add(screen.id, screen);
        }

        ChangeScreen(startScreenId);
    }

    public void ChangeScreen(string screenId)
    {
        ScreenStruct tempScreen;
        if(screenAccess.TryGetValue(screenId, out tempScreen))
        {
            if(currentScreen != null)
                currentScreen.screen.gameObject.SetActive(false);

            currentScreenId = screenId;
            currentScreen = tempScreen;
            currentScreen.screen.gameObject.SetActive(true);
        }
    }

    public void StartLogout()
    {
        MundiPaggSession.Logout();
        ChangeScreen(startScreenId);
    }

    [System.Serializable]
    public class ScreenStruct
    {
        public string id;
        public Screen screen;
    }
}
