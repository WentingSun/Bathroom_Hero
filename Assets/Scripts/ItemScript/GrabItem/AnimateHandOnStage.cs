using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnStage : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator;

    [Header("Player Stage Settings")]
    public string currentPlayerStage; // 当前的playerStage

    // 定义需要手部抓握的特定阶段
    private readonly HashSet<string> grabStages = new HashSet<string> { "playerSelectMop", "playerSelectTubelight" };

    void Update()
    {
        // 检查当前阶段是否为抓握阶段
        if (grabStages.Contains(currentPlayerStage))
        {
            handAnimator.SetFloat("Grip", 1.0f);  // 强制手部抓握
            handAnimator.SetFloat("Trigger", 1.0f);  // 可选：根据需求抓取触发器动画
        }
        else
        {
            // 正常读取输入控制动画
            float triggerValue = pinchAnimationAction.action.ReadValue<float>();
            handAnimator.SetFloat("Trigger", triggerValue);

            float gripValue = gripAnimationAction.action.ReadValue<float>();
            handAnimator.SetFloat("Grip", gripValue);
        }
    }

    // 方法用于更新当前的Player Stage
    public void SetPlayerStage(string newStage)
    {
        currentPlayerStage = newStage;
    }
}
