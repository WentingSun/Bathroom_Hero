using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    private GameState currentGameState;
    public GameState GameStat => currentGameState;
    private PlayerState currentPlayerState;
    public PlayerState PlayerState => currentPlayerState;

    public static event Action<GameState> OnGameStateChange;
    public static event Action<PlayerState> OnPlayerStateChage;

    #region GameState
    public void UpdateGameState(GameState newState){
        currentGameState = newState;
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
    

        private void HandleGameOver(){
        throw new NotImplementedException();
    }

    private void HandleGameStart(){
        throw new NotImplementedException();
    }
    #endregion

    #region PlayerState
    public void UpdatePlayerState(PlayerState newState){
        currentPlayerState = newState;
        switch(newState){
            case PlayerState.playerSelectMop:
            HandleSelectMop();
            break;
            case PlayerState.playerSelectTubelight:

            break;
            case PlayerState.playerWatchMirror:
            
            break;
            case PlayerState.playerDontWatchMirror:
            
            break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);    
        }
        OnPlayerStateChage?.Invoke(newState);
    }
    private void HandleSelectMop(){
        throw new NotImplementedException();
    }
    private void HandleSelectTubelight(){
        throw new NotImplementedException();
    }
    private void HandlePlayerWatchMirror(){
        throw new NotImplementedException();
    }
    private void HandlePlayerDontWathMirror(){
        throw new NotImplementedException();
    }


    #endregion

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


