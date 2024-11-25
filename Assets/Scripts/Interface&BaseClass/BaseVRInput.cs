using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseVRInput : MonoBehaviour, IVRInput
{
    public GameObject Head;
    public GameObject RightHand;
    public GameObject LeftHand;
    public virtual Transform GetHeadPosition()
    {
        throw new System.NotImplementedException();
    }

    public virtual Transform GetLeftHandPosition()
    {
        throw new System.NotImplementedException();
    }

    public virtual Transform GetRightHandPosition()
    {
        throw new System.NotImplementedException();
    }
}
