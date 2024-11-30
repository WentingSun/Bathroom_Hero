using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoastFollow : MonoBehaviour
{

    public Transform cameraTransform; // 摄像机引用
    private Vector3 offset; // 位置偏移量
    [SerializeField]private Quaternion initialRotationDifference; // 初始旋转偏移量
    [SerializeField]float newY;

    void Start()
    {
        // 计算初始位置偏移量
        offset = transform.position - cameraTransform.position;

        // 计算初始旋转的偏移量（镜像旋转）
        // initialRotationDifference = Quaternion.Inverse(cameraTransform.rotation) * transform.rotation;
        // initialRotationDifference = cameraTransform.rotation;
        transform.rotation = initialRotationDifference;
    }

    void Update()
    {
        // // 更新位置：保持与摄像机的相对偏移
        transform.position = cameraTransform.position + offset;

        // // 更新旋转：以镜像方式跟随摄像机
        // // Quaternion mirroredRotation = Quaternion.Inverse(cameraTransform.rotation) * initialRotationDifference;
        Quaternion mirroredRotation = cameraTransform.rotation;
        // Quaternion mirroredRotation = Quaternion.Inverse(cameraTransform.rotation);
        // transform.rotation = initialRotationDifference * mirroredRotation;
        transform.rotation =  initialRotationDifference * mirroredRotation;
        newY = -transform.rotation.y;
        // Debug.Log(newY);
        // transform.rotation =  new Quaternion(transform.rotation.x, newY, transform.rotation.z, transform.rotation.w);
        

        // // 获取当前物体的欧拉角
        // Vector3 currentEulerAngles = transform.eulerAngles;

        // // 对 X 和 Z 轴的旋转取反，Y 轴保持不变
        // Vector3 reversedEulerAngles = new Vector3(-currentEulerAngles.x, currentEulerAngles.y, -currentEulerAngles.z);

        // // 将修改后的欧拉角设置回物体的旋转
        // transform.eulerAngles = reversedEulerAngles;
    }
}