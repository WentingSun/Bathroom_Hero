using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using static UnityEngine.GraphicsBuffer;


public class PipeTubelight : BaseMusicItem
{
    [SerializeField]private bool isSelected = false;
    [SerializeField] private Transform Pivot;
    
    //Pipe Position
    private Vector3 initialPipePosition;
    private Quaternion initialPipeRotation;
    private GameObject pipe;
    [SerializeField] public Vector3 offset;
    //[SerializeField] public Vector3 offsetRotation;
    [SerializeField] public Quaternion offsetRotation;
    
    Transform pivotTransform;

    // Start is called before the first frame update
    void Start()
    {
        pipe = this.gameObject;
        //Initial Position
        initialPipePosition = Pivot.position;
        initialPipeRotation = Pivot.rotation;
        //后面我自己调整内容
        // pivotTransform=pipe.transform.Find("pivot");
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
            Pivot.position = input.GetLeftHandPosition().position + offset;
            //Pivot.rotation = input.GetLeftHandPosition().rotation *  Quaternion.Euler(offsetRotation);
            Pivot.rotation = input.GetLeftHandPosition().rotation *offsetRotation;
            
        }

        // Return to original position
        if (!isSelected)
        {
            Pivot.position = initialPipePosition;
            Pivot.rotation = initialPipeRotation;
        }
    }

    public override void BeSelected()
    {
        isSelected=true;
        beSelected=isSelected;
        Debug.Log("tubelight is selected");
        
    }
    public override void UnSelected()
    {
        isSelected=false;
        beSelected=isSelected;
        
    }
    public bool getIsSelected(){
        return isSelected;
    }
}
