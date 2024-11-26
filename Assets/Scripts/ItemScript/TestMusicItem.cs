using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMusicItem : BaseMusicItem
{
    
    private Vector3 initialPosition; // 用于存储模型的初始位置
    [SerializeField]Vector3 Offset;

    public override void BeSelected()
    {
        beSelected = true;
    }

    public override void UnSelected()
    {

        beSelected =false;

    }



    void Awake()
    {
        beSelected = false;
        // 在脚本初始化时获取并存储模型的初始位置
        initialPosition = this.transform.position;
    }
    // Update is called once per frame
    void Update()
    {

        if(beSelected == true){
            Vector3 targetPosition = input.GetRightHandPosition().position+Offset;
            this.gameObject.transform.position = Vector3.Lerp(
            this.gameObject.transform.position,
            targetPosition,
            Time.deltaTime * 20f // 平滑速度
            );
            //this.gameObject.transform.position = input.GetRightHandPosition().position+Offset;
        }
        if(beSelected == false){
            Vector3 targetPosition = initialPosition;
            this.gameObject.transform.position = Vector3.Lerp(
            this.gameObject.transform.position,
            targetPosition,
            Time.deltaTime * 20f // 平滑速度
            );
        }
    }
}
