using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopGuitar : BaseMusicItem
{
    [SerializeField]Transform Mop;
    [SerializeField]Transform FixPoint;
    [SerializeField]bool isSelected;
    [SerializeField] private GuitarMode guitarMode;
    [SerializeField] private float ModeARange;
    [SerializeField] private float ModeBRange;
    [SerializeField] private float ModeCRange;
    public Vector3 rotationOffset;
    float distanseOfLeftHand=>getDistanceOfHand();
    
    
    private void OnTriggerEnter(Collider other){
        AudioManager.Instance.PlaySound("TestSound");
    }

    public override void BeSelected()
    {
        isSelected = true;
        Debug.Log("Mop be selected");
    }

    public override void UnSelected()
    {
        isSelected = false;
        Debug.Log("Mop be unselected");
    }

    private float getDistanceOfHand(){
        return Vector3.Distance(FixPoint.position,input.GetLeftHandPosition().position);
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckAttribute();
    }

private void CheckAttribute()
{
    if (!(ModeCRange > ModeARange && ModeBRange > ModeARange && ModeCRange > ModeBRange))
    {
        Debug.Log("Range Mode Attribute has problem");
    }

    
}


    // Update is called once per frame
    void Update()
    {
        if(distanseOfLeftHand < ModeARange){
            guitarMode=GuitarMode.A;
        }else if(distanseOfLeftHand < ModeBRange){
            guitarMode=GuitarMode.B;
        }else if(distanseOfLeftHand < ModeCRange){
            guitarMode=GuitarMode.C;
        }
        if(isSelected){
            Vector3 direction = input.GetLeftHandPosition().position -FixPoint.position;
            Mop.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(rotationOffset);
        }
        // Debug.Log(distanseOfLeftHand);
    }
}

public enum GuitarMode{
    A,
    B,
    C
}
