using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMove : MonoBehaviour
{
    public Camera mainCamera; // 主摄像机，用于将屏幕坐标转换为世界坐标
    public float zDistance = 10f; // 距离摄像机的深度

    void Update()
    {
        // 获取鼠标在屏幕上的位置
        Vector3 mousePosition = Input.mousePosition;

        // 将屏幕坐标转换为世界坐标
        mousePosition.z = zDistance; // 设置深度
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // 更新手部位置为鼠标位置
        transform.position = worldPosition;
    }
}
