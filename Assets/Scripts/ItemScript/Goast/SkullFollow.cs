using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullFollow : MonoBehaviour
{
    [Header("VR头显位置")]
    public Transform vrBasicMainCameraTransform;
    public Transform vrRControllerTransform;
    public Transform vrLControllerTransform;

    [Header("Skull")]
    public Transform skullHead;
    public Transform skullRHand;
    public Transform skullLHand;
    public Transform skullBody;

    [Header("摄像机初始位置")]
    public Transform cameraFollowTransform;
    public Vector3 offset;
    public Vector3 rightHandOffset;
    public Vector3 leftHandOffset;
    // Start is called before the first frame update
    [Header("自己设定的手柄偏移")]
    public Vector3 BothHandOffset;
    public float ControllerOffset;
    void Start()
    {
        // 计算初始位置偏移量
        offset = transform.position - cameraFollowTransform.position;
        vrRControllerTransform=GameObject.Find("[Right Controller] Model Parent").transform;
        vrLControllerTransform=GameObject.Find("[Left Controller] Model Parent").transform;
        rightHandOffset=skullRHand.position-vrRControllerTransform.position;
        leftHandOffset=skullLHand.position-vrLControllerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HeadTransformFollow(); 
        BasicTransformFollow();
        vrRControllerTransform=GameObject.Find("[Right Controller] Model Parent").transform;
        vrLControllerTransform=GameObject.Find("[Left Controller] Model Parent").transform;
        HandTransformFollow(skullLHand,vrLControllerTransform,leftHandOffset);
        HandTransformFollow(skullRHand,vrRControllerTransform,rightHandOffset);
    }
    void HandTransformFollow(Transform SkullHand,Transform ControllerTransform,Vector3 HandOffset){
        // 获取摄像机的当前位置
        //Vector3 cameraPosition = cameraFollowTransform.position;
        Vector3 ContollerPosition=ControllerTransform.position;
        // 保持Cube的X轴位置不变，Y轴和Z轴位置跟随摄像机
        SkullHand.position = new Vector3(ContollerPosition.x + HandOffset.x+BothHandOffset.x, ContollerPosition.y + HandOffset.y+BothHandOffset.y, ContollerPosition.z + HandOffset.z+BothHandOffset.z)*ControllerOffset;
         // 获取摄像机的旋转
        Quaternion ContollerRotation = ControllerTransform.rotation;

        // 提取摄像机的欧拉角
        Vector3 controllerEulerAngles = ContollerRotation.eulerAngles;

        // 分离 Y 轴旋转（反向）和 X-Z 平面旋转（同步）
        Quaternion yRotation = Quaternion.Euler(0, controllerEulerAngles.y, 0); // 绕Y 轴旋转
        Quaternion xzRotation = Quaternion.Euler(controllerEulerAngles.x, 0, controllerEulerAngles.z); // X 和 Z 不变

        // 组合旋转
        Quaternion finalRotation = yRotation * xzRotation;

        // 应用到 Cube 的旋转
        SkullHand.rotation = finalRotation;
    }
    void HeadTransformFollow(){
        // 获取摄像机的当前位置
        Vector3 cameraPosition = cameraFollowTransform.position;
        // 保持Cube的X轴位置不变，Y轴和Z轴位置跟随摄像机
        transform.position = new Vector3(cameraPosition.x + offset.x, cameraPosition.y + offset.y, cameraPosition.z + offset.z);
         // 获取摄像机的旋转
        Quaternion cameraRotation = cameraFollowTransform.rotation;

        // 提取摄像机的欧拉角
        Vector3 cameraEulerAngles = cameraRotation.eulerAngles;

        // 分离 Y 轴旋转（反向）和 X-Z 平面旋转（同步）
        Quaternion yRotation = Quaternion.Euler(0, cameraEulerAngles.y, 0); // 绕Y 轴旋转
        Quaternion xzRotation = Quaternion.Euler(cameraEulerAngles.x, 0, cameraEulerAngles.z); // X 和 Z 不变

        // 组合旋转
        Quaternion finalRotation = yRotation * xzRotation;

        // 应用到 Cube 的旋转
        skullHead.rotation = finalRotation;
    }
     void BasicTransformFollow(){
        // 获取摄像机的当前位置
        Vector3 cameraPosition = cameraFollowTransform.position;
        // 保持Cube的X轴位置不变，Y轴和Z轴位置跟随摄像机
        transform.position = new Vector3(cameraPosition.x + offset.x, cameraPosition.y + offset.y, cameraPosition.z + offset.z);
        // 获取摄像机的旋转
        Quaternion cameraRotation = cameraFollowTransform.rotation;

        // 提取摄像机的欧拉角
        Vector3 cameraEulerAngles = cameraRotation.eulerAngles;

        // 只保留 Y 轴的旋转，去掉 X 和 Z 轴的旋转
        float yRotationAngle = cameraEulerAngles.y;

        // 创建绕 Y 轴的旋转
        Quaternion yRotation = Quaternion.Euler(0, yRotationAngle, 0);

        // 应用到 Cube 的旋转
        skullBody.rotation = yRotation;
    }
}
