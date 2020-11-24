using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnStartButtonClick()
    {
        GameMgr.Instance.OnStartGame();
    }

    public void OnExitButtonClick()
    {
        GameMgr.Instance.OnExitGame();
    }
}
