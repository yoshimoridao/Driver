using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuConfig", menuName = "ScriptableObjects/MenuConfig", order = 1)]
public class MenuConfig : ScriptableObject
{
    public class MenuInfo
    {
        public UIManager.MenuType menuType;
        public GameObject obj;

        public MenuInfo()
        {
        }

        public MenuInfo(MenuInfo clone)
        {
            this.menuType = clone.menuType;
            this.obj = Instantiate(clone.obj);
        }
    }

    public List<MenuInfo> menuInfos = new List<MenuInfo>();

    public MenuInfo GetMenuInfo(UIManager.MenuType type)
    {
        return menuInfos.Find(x => x.menuType == type);
    }
}
