using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMusicItem : BaseMusicItem
{
    [SerializeField]
    private bool beSelected;

    public override void BeSelected()
    {
        beSelected = true;
    }

    public override void UnSelected()
    {
        beSelected = false;
        Debug.Log("Method:UnSelected is used");
        AudioManager.Instance.PlaySound("TestSound");
    }



    void Awake()
    {
        beSelected = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (beSelected == true)
        {
            this.gameObject.transform.position = input.GetRightHandPosition().position;
        }
    }
}
