using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoastFollow : MonoBehaviour
{

    public Transform cameraTransform; // 摄像机引用
    private Vector3 offset; // 位置偏移量
    private Quaternion initialRotationDifference; // 初始旋转偏移量

    void Start()
    {
        // 计算初始位置偏移量
        offset = transform.position - cameraTransform.position;

        // 计算初始旋转的偏移量（镜像旋转）
        initialRotationDifference = Quaternion.Inverse(cameraTransform.rotation) * transform.rotation;
    }

    void Update()
    {
        // 更新位置：保持与摄像机的相对偏移
        transform.position = cameraTransform.position + offset;

        // 更新旋转：以镜像方式跟随摄像机
        Quaternion mirroredRotation = Quaternion.Inverse(cameraTransform.rotation) * initialRotationDifference;
        transform.rotation = mirroredRotation;
    }
}