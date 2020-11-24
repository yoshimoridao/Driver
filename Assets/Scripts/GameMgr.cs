using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance;
    public enum GameState { Start, Play, GameOver };
    private GameState gameState;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameState = GameState.Start;       
    }

    void Update()
    {
        
    }

    public void OnStartGame()
    {

    }

    public void OnExitGame()
    {
        Application.Quit();
    }
}
