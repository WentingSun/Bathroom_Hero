using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    private GameState state;
    public GameState GameStat => state;

    public static event Action<GameState> OnGameStateChange;
    public static event Action<PlayerState> OnPlayerStateChage;
    
    public void UpdateGameState(GameState newState){
        state = newState;
        switch(newState) {
            case GameState.GameStart:
                HandleGameStart();
            break;
            case GameState.GameOver:
                HandleGameOver();
            break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState , null);

        }
        OnGameStateChange?.Invoke(newState);
    }

    private void HandleGameOver()
    {
        throw new NotImplementedException();
    }

    private void HandleGameStart()
    {
        throw new NotImplementedException();
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
