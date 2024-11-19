using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopGuitar : BaseMusicItem
{
    
    
    private void OnTriggerEnter(Collider other){
        
        AudioManager.Instance.PlaySound("TestSound");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
