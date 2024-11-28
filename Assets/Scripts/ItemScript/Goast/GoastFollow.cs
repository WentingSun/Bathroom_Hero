using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoastFollow : MonoBehaviour
{

    public Transform cameraTransform; // 摄像机引用
    [SerializeField]private Vector3 offset; // 位置偏移量
    [SerializeField]private Quaternion initialRotationDifference; // 初始旋转偏移量
 
    float unchangableX;

    void Awake(){
        // 计算初始位置偏移量
        offset = transform.position - cameraTransform.position;

        // 计算初始旋转的偏移量（镜像旋转）
        transform.rotation = initialRotationDifference;
        //
        transform.position=cameraTransform.position+offset;
    }
    void Start()
    {
        
        //
        unchangableX=transform.position.x;
    }

    void Update()
    {
        //transform.position=cameraTransform.position+offset;
        // 获取摄像机的当前位置
        Vector3 cameraPosition = cameraTransform.position;

        // 保持Cube的X轴位置不变，Y轴和Z轴位置跟随摄像机
        transform.position = new Vector3(unchangableX, cameraPosition.y + offset.y, cameraPosition.z + offset.z);
        
        // 获取摄像机的旋转
        Quaternion cameraRotation = cameraTransform.rotation;

        // 提取摄像机的欧拉角
        Vector3 cameraEulerAngles = cameraRotation.eulerAngles;

        // 分离 Y 轴旋转（反向）和 X-Z 平面旋转（同步）
        Quaternion yRotation = Quaternion.Euler(0, -cameraEulerAngles.y, 0); // 绕Y 轴旋转
        Quaternion xzRotation = Quaternion.Euler(cameraEulerAngles.x, 0, cameraEulerAngles.z); // X 和 Z 不变

        // 组合旋转
        Quaternion finalRotation = yRotation * xzRotation;

        // 应用到 Cube 的旋转
        transform.rotation = finalRotation;
    }
}