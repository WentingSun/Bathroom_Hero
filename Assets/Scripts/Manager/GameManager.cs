using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnGameStateChange;
    public static event Action<PlayerState> OnPlayerStateChage;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

public enum GameState{
    GameStart,
    GameOver
}

public enum PlayerState{
    playerSelectMop,
    playerSelectTubelight,
    playerWatchMirror,
    playerDontWatchMirror
}
