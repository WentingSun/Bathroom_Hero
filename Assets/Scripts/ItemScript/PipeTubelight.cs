using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.XR;


public class PipeTubelight : BaseMusicItem
{
    private bool isSelected = false;
    
    //Pipe Position
    private Vector3 initialPipePosition;
    private Quaternion initialPipeRotation;
    private GameObject pipe;
    [SerializeField] public Vector3 offset;
    
    private bool isFollowLeftHand = false;
    private bool isFollowRightHand = false;
    

    [SerializeField]private InputDevice RightHandController; // 用于存储左手控制器的输入设备
    public BaseMusicItem musicItem; // 引用 TestMusicItem 脚本
    //private bool isSelected = false; // 记录当前是否选中状态
    private bool isCoolingDown = false; // 冷却状态标记
    [SerializeField] private float coolDownTime = 1f; // 冷却时间（秒）


    // Start is called before the first frame update
    void Start()
    {
        pipe = this.gameObject;
        //Initial Position
        initialPipePosition = pipe.transform.position;
        initialPipeRotation = pipe.transform.rotation;

        //Cotroller Position
        // Vector3 inputHeadPosition = input.GetHeadPosition().position;
        // Vector3 inputLeftHandPosition = input.GetLeftHandPosition().position;
        // Vector3 inputRightHandPosition = input.GetRightHandPosition().position;
        // Vector3 inputHeadPosition = input.GetHeadPosition().position;
        // Vector3 inputRightHandControllerPosition= input.GetRightHandPosition().position;
        // Vector3 inputLeftHandPosition = input.GetRightHandPosition().position;

        // 获取左手手柄设备
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);//
        Debug.Log(devices == null);
        Debug.Log(devices.Count);
        if (devices.Count == 0 &&  devices != null)
        {
            RightHandController = devices[0];
            Debug.Log("Right-hand controller found!");
        }
        else
        {
            Debug.LogError("No Right-hand controller detected!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        followHandController();
    }

    // TODO: Change the Controller Key code
    public void followHandController()
    {
        // 确保左手手柄存在
        if ( RightHandController.isValid)
        {
            // 检查左手的 Trigger 键是否被按下
            if ( RightHandController.TryGetFeatureValue(CommonUsages.triggerButton, out bool isPressed) && isPressed)
            {
                if (pipe != null && input != null)
            {
                BeSelected();
                isFollowLeft();
            }
            }
        }
        // Z Follow Left Controller
        // if (Input.GetKeyDown(KeyCode.Z))
        // {
        //     if (pipe != null && input != null)
        //     {
        //         BeSelected();
        //         isFollowLeft();
        //     }
        // }
        // X Follow Right Controller
        // if (Input.GetKeyDown(KeyCode.X))
        // {
        //     if (pipe != null && input != null)
        //     {
        //         BeSelected();
        //         isFollowRight();
        //     }
        // }

        if (isSelected && isFollowLeftHand)
        {
            pipe.transform.position = input.GetLeftHandPosition().position + offset;
            pipe.transform.rotation = input.GetLeftHandPosition().rotation;
        }

        if (isSelected && isFollowRightHand)
        {
            pipe.transform.position = input.GetRightHandPosition().position  + offset;
            pipe.transform.rotation = input.GetRightHandPosition().rotation;

        }

        // Return to original position
        if (!isSelected)
        {
            pipe.transform.position = initialPipePosition;
            pipe.transform.rotation = initialPipeRotation;
            isFollowLeftHand = false;
            isFollowRightHand = false;
        }
    }

    public override void BeSelected()
    {
        isSelected=!isSelected;
        //
        beSelected=isSelected;
        
    }
    public void isFollowRight()
    {
        isFollowRightHand = true;
        isFollowLeftHand = false;
    }
    public void isFollowLeft()
    {
        isFollowLeftHand = true;
        isFollowRightHand = false;
    }
    public bool getIsSelected(){
        return isSelected;
    }
}
