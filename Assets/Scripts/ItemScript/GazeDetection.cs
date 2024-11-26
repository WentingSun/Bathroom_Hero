using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform mirror; // 镜子的Transform对象
    public GameManager gameManager; // 引用 GameManager
    public float gazeThreshold = 0.9f; // 点积阈值，表示视线与镜子方向的接近程度

    void Update()
    {
        // 获取摄像头的前向方向（即用户的视线方向）
        Vector3 headDirection = Camera.main.transform.forward;

        // 计算镜子的方向向量
        Vector3 mirrorDirection = (mirror.position - Camera.main.transform.position).normalized;

        // 使用点积来判断视线方向是否接近镜子
        float dotProduct = Vector3.Dot(headDirection, mirrorDirection);

        if (dotProduct > gazeThreshold)
        {
            // Debug.Log("用户正在看向镜子");
            // GameManager.Instance.UpdatePlayerState(PlayerState.playerWatchMirror);
        }
        else
        {
            // Debug.Log("用户没有看向镜子");
            // GameManager.Instance.UpdatePlayerState(PlayerState.playerDontWatchMirror);
        }
    }
}
