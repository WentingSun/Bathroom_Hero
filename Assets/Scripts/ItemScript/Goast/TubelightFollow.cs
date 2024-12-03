using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubelightFollow : MonoBehaviour
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
    public Transform LeftHandTransform;
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
        Pivot.position = LeftHandTransform.position + offset;
            //Pivot.rotation = input.GetLeftHandPosition().rotation *  Quaternion.Euler(offsetRotation);
            Pivot.rotation = LeftHandTransform.rotation *offsetRotation;

        // Return to original position
        
    }

    

}
