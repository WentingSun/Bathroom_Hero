using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputOnTrigger : MonoBehaviour
{

    public InputActionProperty triggerButton;
    public BaseMusicItem musicItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerPressValue = triggerButton.action.ReadValue<float>();

        if(triggerPressValue > 0.1)
        {
            Debug.Log("Trigger Pressed");
            musicItem.BeSelected();
        }
    }
}

