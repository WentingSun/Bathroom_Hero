using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeDetection : MonoBehaviour
{
    public Transform mirror; // 镜子的Transform对象
    public float gazeThreshold = 0.9f; // 点积阈值，表示视线与镜子方向的接近程度

    private PlayerState currentState = PlayerState.PlayerSelectNothing; // 当前玩家状态，初始为未定义状态

    void Update()
    {
        // 获取摄像头的前向方向（即用户的视线方向）
        Vector3 headDirection = Camera.main.transform.forward;

        // 计算镜子的方向向量
        Vector3 mirrorDirection = (mirror.position - Camera.main.transform.position).normalized;

        // 使用点积来判断视线方向是否接近镜子
        float dotProduct = Vector3.Dot(headDirection, mirrorDirection);

        // 根据点积计算是否看向镜子
        PlayerState newState = dotProduct > gazeThreshold 
            ? PlayerState.playerWatchMirror 
            : PlayerState.playerDontWatchMirror;

        // 只有当状态发生变化时才更新状态
        if (newState != currentState)
        {
            currentState = newState; // 更新当前状态
            GameManager.Instance.UpdatePlayerState(currentState); // 通知 GameManager 更新状态

            // 日志输出用于调试
            Debug.Log(newState == PlayerState.playerWatchMirror 
                ? "用户正在看向镜子" 
                : "用户没有看向镜子");
        }
    }
}
