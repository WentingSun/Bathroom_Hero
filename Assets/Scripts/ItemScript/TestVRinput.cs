using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVRinput:BaseVRInput
{


    void Start()
    {
        LeftHand = GameObject.Find("[Left Controller] Model Parent");
        RightHand = GameObject.Find("[Right Controller] Model Parent");
    }

    public override Transform GetHeadPosition()
    {
        return Head.gameObject.transform;
    }

    public override Transform GetLeftHandPosition()
    {
        return LeftHand.gameObject.transform;
    }

    public override Transform GetRightHandPosition()
    {
        return RightHand.gameObject.transform;
    }
}
