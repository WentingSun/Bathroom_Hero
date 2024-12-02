using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameState currentGameState;
    public GameState GameStat => currentGameState;
    [SerializeField] private PlayerState currentPlayerState;
    public PlayerState PlayerState => currentPlayerState;
    [SerializeField] private SelectedItem currentSelectedItem;
    public SelectedItem SelectedItem => currentSelectedItem;

    public static event Action<GameState> OnGameStateChange;
    public static event Action<PlayerState> OnPlayerStateChage;
    private PlayerState playerState = PlayerState.PlayerSelectNothing;

    #region GameState
    public void UpdateGameState(GameState newState)
    {
        currentGameState = newState;
        switch (newState)
        {
            case GameState.GameStart:
                HandleGameStart();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

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
    #endregion

    #region PlayerState
    public void UpdatePlayerState(PlayerState newState)
    {
        currentPlayerState = newState;
        switch (newState)
        {
            case PlayerState.playerSelectMop:
                HandleSelectMop();
                break;
            case PlayerState.playerSelectTubelight:
                HandleSelectTubelight();
                break;
            case PlayerState.PlayerSelectNothing:
                HandleSelectNothing();
                break;
            case PlayerState.playerWatchMirror:
                HandlePlayerWatchMirror();
                break;
            case PlayerState.playerDontWatchMirror:
                HandlePlayerDontWathMirror();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnPlayerStateChage?.Invoke(newState);
    }

    private void HandleSelectNothing()
    {
        currentSelectedItem = SelectedItem.Nothing;
        // throw new NotImplementedException();
    }

    private void HandleSelectMop()
    {
        currentSelectedItem = SelectedItem.Mop;
        // throw new NotImplementedException();
    }
    private void HandleSelectTubelight()
    {
        currentSelectedItem = SelectedItem.Tubelight;
        // throw new NotImplementedException();
    }
    private void HandlePlayerWatchMirror()
    {
        // throw new NotImplementedException();
        Debug.Log("Player Watch Mirror now");
    }
    private void HandlePlayerDontWathMirror()
    {
        // throw new NotImplementedException();
        Debug.Log("Player Dont Watch Mirror now");
    }


    #endregion

}

public enum GameState
{
    GameStart,
    GameOver
}

public enum PlayerState
{
    playerSelectMop,
    playerSelectTubelight,
    PlayerSelectNothing,
    playerWatchMirror,
    playerDontWatchMirror
}

public enum SelectedItem
{
    Mop,
    Tubelight,
    Nothing
}


