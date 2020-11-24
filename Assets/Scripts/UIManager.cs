using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    public static Action onStartButtonClick;

    public enum MenuType { MainMenu, Play, GameOver, Win };
    public MenuConfig menuConfig;

    private MenuConfig.MenuInfo curMenuInfo;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PushMenu(MenuType type)
    {
        var prefMenu = menuConfig.GetMenuInfo(type);

        // remove current menu
        if (curMenuInfo != null)
            Destroy(curMenuInfo.obj);

        // push new menu
        curMenuInfo = new MenuConfig.MenuInfo(prefMenu);
        curMenuInfo.obj.transform.parent = transform;
    }


}
