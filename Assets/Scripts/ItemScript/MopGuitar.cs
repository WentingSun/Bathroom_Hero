using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopGuitar : BaseMusicItem
{
    [SerializeField]Transform Mop;
    [SerializeField]Transform FixPoint;
    [SerializeField]bool isSelected;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelected){
            Vector3 direction = input.GetLeftHandPosition().position -FixPoint.position;
            Mop.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(rotationOffset);
        }
        // Debug.Log(distanseOfLeftHand);
    }
}
