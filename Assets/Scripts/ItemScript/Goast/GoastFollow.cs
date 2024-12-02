using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class GoastFollow : MonoBehaviour
{
    //Head
    public Transform cameraFollowTransform; // 渲染摄像机引用

    public Transform vrBasicMainCameraTransform;
    public Transform vrRControllerTransform;
    public Transform vrLControllerTransform;
    //transform引用整体
    [SerializeField]private Transform headTransform;//头部
    [SerializeField]private Transform lHandTransform;//左手
    InputDevice leftHandController;
    [SerializeField]private Transform rHandTransform;//右手
    [SerializeField]private Transform bodyTransform;
    [SerializeField]private Vector3 offset; // 位置偏移量
    [SerializeField]private Quaternion initialRotationDifference; // 初始旋转偏移量

    Vector3 LeftControllerOffset;
    Transform OLeftControllerTransform;
    Vector3 initPosition;

    //Hand

 
    float unchangableX;


    void Awake(){
        
    }
    void Start()
    { 
        // 计算初始位置偏移量
        offset = transform.position - cameraFollowTransform.position;
        //
        unchangableX=transform.position.x;
        //
        transform.rotation = initialRotationDifference;
        //
        //transform.position=cameraTransform.position+offset;
        //TransformFollow();
        //originalLeftOffset=vrBasicMainCameraTransform.position-vrLControllerTransform.position;
        OLeftControllerTransform=GameObject.Find("[Left Controller] Model Parent").transform;
        initPosition = OLeftControllerTransform.position;
        
    }

    void Update()
    {
        HeadTransformFollow(); 
        BasicTransformFollow(bodyTransform);
        //HandTransformFollow(RHandTransform);
        
        // leftOffset=vrBasicMainCameraTransform.position-vrLControllerTransform.position;
        // Debug.Log("OFFSET:"+leftOffset);
        LeftHandTransformFollow();
        
    }
    void LeftHandMoveDecect(){
        vrLControllerTransform=GameObject .Find("[Left Controller] Model Parent").transform;
        if(initPosition!=vrLControllerTransform.position){
            LeftControllerOffset=vrLControllerTransform.position-cameraFollowTransform.position;
        }else{
            Debug.Log("没有变化");
        }
    
        
    }
    void LeftHandTransformFollow(){
        //得到偏移LeftControllerOffset
        LeftHandMoveDecect();
        Debug.Log(LeftControllerOffset);
        // 获取骷髅
        lHandTransform.position=new Vector3(headTransform.position.x-LeftControllerOffset.x,
            headTransform.position.y+LeftControllerOffset.y,
            headTransform.position.z-LeftControllerOffset.z);
        
        //originalLeftOffset=originalLeftOffset;
        
        // 获取摄像机的旋转
        Quaternion cameraRotation = cameraFollowTransform.rotation;

        // 提取摄像机的欧拉角
        Vector3 cameraEulerAngles = cameraRotation.eulerAngles;

        // 只保留 Y 轴的旋转，去掉 X 和 Z 轴的旋转
        float yRotationAngle = cameraEulerAngles.y;

        // 创建绕 Y 轴的旋转
        Quaternion yRotation = Quaternion.Euler(0, -yRotationAngle, 0);

        // 应用到 Cube 的旋转
        lHandTransform.rotation = yRotation;

        
        }

    void BasicTransformFollow(Transform Basic){
        // 获取摄像机的当前位置
        Vector3 cameraPosition = cameraFollowTransform.position;
        // 保持Cube的X轴位置不变，Y轴和Z轴位置跟随摄像机
        transform.position = new Vector3(unchangableX, cameraPosition.y + offset.y, cameraPosition.z + offset.z);
        // 获取摄像机的旋转
        Quaternion cameraRotation = cameraFollowTransform.rotation;

        // 提取摄像机的欧拉角
        Vector3 cameraEulerAngles = cameraRotation.eulerAngles;

        // 只保留 Y 轴的旋转，去掉 X 和 Z 轴的旋转
        float yRotationAngle = cameraEulerAngles.y;

        // 创建绕 Y 轴的旋转
        Quaternion yRotation = Quaternion.Euler(0, -yRotationAngle, 0);

        // 应用到 Cube 的旋转
        Basic.rotation = yRotation;
    }

    void HeadTransformFollow(){
        // 获取摄像机的当前位置
        Vector3 cameraPosition = cameraFollowTransform.position;
        // 保持Cube的X轴位置不变，Y轴和Z轴位置跟随摄像机
        transform.position = new Vector3(unchangableX, cameraPosition.y + offset.y, cameraPosition.z + offset.z);
         // 获取摄像机的旋转
        Quaternion cameraRotation = cameraFollowTransform.rotation;

        // 提取摄像机的欧拉角
        Vector3 cameraEulerAngles = cameraRotation.eulerAngles;

        // 分离 Y 轴旋转（反向）和 X-Z 平面旋转（同步）
        Quaternion yRotation = Quaternion.Euler(0, -cameraEulerAngles.y, 0); // 绕Y 轴旋转
        Quaternion xzRotation = Quaternion.Euler(cameraEulerAngles.x, 0, cameraEulerAngles.z); // X 和 Z 不变

        // 组合旋转
        Quaternion finalRotation = yRotation * xzRotation;

        // 应用到 Cube 的旋转
        headTransform.rotation = finalRotation;
    }
}