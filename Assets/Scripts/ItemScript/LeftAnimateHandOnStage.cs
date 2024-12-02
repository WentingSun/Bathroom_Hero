using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeftAnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator;

    // Update is called once per frame
    void Update()
    {
        // 获取当前玩家状态
        PlayerState playerState = GameManager.Instance.PlayerState;

        // 判断是否为 playerSelectMop 或 playerSelectTubelight 状态
        if (playerState == PlayerState.playerSelectMop || playerState == PlayerState.playerSelectTubelight)
        {
            handAnimator.SetFloat("Grip", 1f); // 持续保持 Grip 动画
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
