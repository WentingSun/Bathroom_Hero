using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using static UnityEngine.GraphicsBuffer;


public class PipeTubelight : BaseMusicItem
{
    private bool isSelected = false;
    
    //Pipe Position
    private Vector3 initialPipePosition;
    private Quaternion initialPipeRotation;
    private GameObject pipe;
    [SerializeField] public Vector3 offset;
    
    Transform pivotTransform;

    // Start is called before the first frame update
    void Start()
    {
        pipe = this.gameObject;
        //Initial Position
        initialPipePosition = pipe.transform.position;
        initialPipeRotation = pipe.transform.rotation;
        //后面我自己调整内容
        pivotTransform=pipe.transform.Find("pivot");

    }

    // Update is called once per frame
    void Update()
    {
        followHandController();
    }

    public void followHandController()
    {
        if (isSelected)
        {
            pipe.transform.position = input.GetLeftHandPosition().position + offset;
            pipe.transform.rotation = input.GetLeftHandPosition().rotation;
        }

        // Return to original position
        if (!isSelected)
        {
            pipe.transform.position = initialPipePosition;
            pipe.transform.rotation = initialPipeRotation;
        }
    }

    public override void BeSelected()
    {
        isSelected=!isSelected;
        beSelected=isSelected;
        
    }
    public override void UnSelected()
    {
        isSelected=!isSelected;
        beSelected=isSelected;
        
    }
    public bool getIsSelected(){
        return isSelected;
    }
}
