using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class PipeTubelight : BaseMusicItem
{
    private bool isSeleced = false;
    
    //跟随手柄
    private Vector3 initialPipePosition;
    private Quaternion initialPipeRotation;
    private GameObject pipe;
    [SerializeField] public Vector3 offset;
    
    private bool isFollowLeftHand = false;
    private bool isFollowRightHand = false;


    // Start is called before the first frame update
    void Start()
    {
        pipe = this.gameObject;
        //Initial Position
        initialPipePosition = pipe.transform.position;
        initialPipeRotation = pipe.transform.rotation;

        //Cotroller Position
        Vector3 inputHeadPosition = input.GetHeadPosition().position;
        Vector3 inputLeftHandPosition = input.GetLeftHandPosition().position;
        Vector3 inputRightHandPosition = input.GetRightHandPosition().position;
    }

    // Update is called once per frame
    void Update()
    {
        followHandController();
    }

    // TODO: Change the Controller Key code
    public void followHandController()
    {
        // 按下 Z 键时，左手控制/放下

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (pipe != null && input != null)
            {
                BeSelected();
                isFollowLeft();
            }
        }
        // 按下 X 键时，右手控制/放下
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (pipe != null && input != null)
            {
                BeSelected();
                isFollowRight();
            }
        }

        if (isSeleced && isFollowLeftHand)
        {
            pipe.transform.position = input.GetLeftHandPosition().position + offset;
            pipe.transform.rotation = input.GetLeftHandPosition().rotation;
        }

        if (isSeleced && isFollowRightHand)
        {
            pipe.transform.position = input.GetRightHandPosition().position + offset;
            pipe.transform.rotation = input.GetRightHandPosition().rotation;

        }

        // 回到原位
        if (!isSeleced)
        {
            pipe.transform.position = initialPipePosition;
            pipe.transform.rotation = initialPipeRotation;
            isFollowLeftHand = false;
            isFollowRightHand = false;
        }
    }

    public override void BeSelected()
    {
        isSeleced=!isSeleced;

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
}
