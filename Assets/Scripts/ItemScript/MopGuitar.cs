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
    [SerializeField] private int soundIndex;
    public Vector3 rotationOffset;
    float distanseOfLeftHand=>getDistanceOfHand();
    
    
    private void OnTriggerEnter(Collider other){
        AudioManager.Instance.PlaySound(ToString(guitarMode,soundIndex));
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
        soundIndex=0;
        Debug.Log(ToString(GuitarMode.A,7));
        
    }

    private void CheckAttribute()
    {
        if (!(ModeCRange > ModeARange && ModeBRange > ModeARange && ModeCRange > ModeBRange))
        {
            Debug.Log("Range Mode Attribute has problem");
        }

        
    }

    private static string ToString(GuitarMode mode,int index){
        string result= "";
        switch(mode){
            case GuitarMode.A:
            result += "ModeA";
            break;
            case GuitarMode.B:
            result += "ModeB";
            break;
            case GuitarMode.C:
            result += "ModeC";
            break;
        }
        result += index.ToString("D2");
        return result;
        //example for mode A with 7 index
        //ModeA07
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
