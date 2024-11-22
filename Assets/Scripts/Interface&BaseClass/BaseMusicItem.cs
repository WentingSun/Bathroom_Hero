using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMusicItem : MonoBehaviour, IMusicItem
{
    public bool beSelected;
    [SerializeField] public TestVRinput input;
    public virtual void BeSelected()
    {
        throw new System.NotImplementedException();
    }

    public virtual void UnSelected()
    {
        throw new System.NotImplementedException();
    }

    public virtual void PlayMusic()
    {
        throw new System.NotImplementedException();
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
