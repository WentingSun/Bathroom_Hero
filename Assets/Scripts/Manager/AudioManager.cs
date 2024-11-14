using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    protected override void Awake(){
        base.Awake();
       GameManager.OnGameStateChange += audioManagerOnGameStateChange;
       GameManager.OnPlayerStateChage += audioManagerOnPlayerStateChange;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.OnGameStateChange -= audioManagerOnGameStateChange;
        GameManager.OnPlayerStateChage -= audioManagerOnPlayerStateChange;
    }

    private void audioManagerOnGameStateChange(GameState gameState){
        throw new System.NotImplementedException();
    }

    private void audioManagerOnPlayerStateChange(PlayerState playerState){
        throw new System.NotImplementedException();
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
