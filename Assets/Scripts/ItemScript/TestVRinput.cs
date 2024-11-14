using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVRinput : BaseVRInpute
{

    public override Transform GetHeadPosition(){
        return Head.gameObject.transform;
    }

    public override Transform GetLeftHandPosition(){
        return LeftHand.gameObject.transform;
    }

    public override Transform GetRightHandPosition(){
        return RightHand.gameObject.transform;
    }
}
