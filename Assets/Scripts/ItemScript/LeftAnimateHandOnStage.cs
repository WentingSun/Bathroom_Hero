using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeftAnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    [SerializeField] private PlayerState watchingState;
    [SerializeField] private PlayerState selectedItem = PlayerState.PlayerSelectNothing;
    public Animator handAnimator;


    void Awake()
    {
        selectedItem = PlayerState.PlayerSelectNothing;
        GameManager.OnPlayerStateChage += OnPlayerStateChage;
    }
    void OnDestroy(){
        GameManager.OnPlayerStateChage -= OnPlayerStateChage;
    }
    void OnPlayerStateChage(PlayerState playerState){
        if (playerState == PlayerState.playerWatchMirror || playerState == PlayerState.playerDontWatchMirror)
        {
            watchingState = playerState;
        }else if(playerState == PlayerState.playerSelectTubelight || playerState == PlayerState.playerSelectMop || playerState == PlayerState.PlayerSelectNothing){
            selectedItem = playerState;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 获取当前玩家状态
        

        // 判断是否为 playerSelectMop 或 playerSelectTubelight 状态
        if (selectedItem == PlayerState.playerSelectMop || selectedItem == PlayerState.playerSelectTubelight)
        {
            handAnimator.SetFloat("Grip", 1f); // 持续保持 Grip 动画
            // Debug.Log("Grab animation");
        }
        else
        {
            float gripValue = gripAnimationAction.action.ReadValue<float>();
            handAnimator.SetFloat("Grip", gripValue);
        }

        // 实时更新 Pinch 动画参数
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);
    }
}
