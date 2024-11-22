using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMusicItem : BaseMusicItem
{
    [SerializeField]
    private bool beSelected;
    [SerializeField]Vector3 Offset;

    public override void BeSelected()
    {
        beSelected = true;

        // 获取模型的 Renderer 组件
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            // 修改材质的颜色为黄色
            renderer.material.color = Color.yellow;

            Debug.Log($"{gameObject.name} color changed to yellow!");
        }
        else
        {
            Debug.LogWarning("Renderer not found on this object.");
        }

    }

    public override void UnSelected()
    {
        beSelected =false;

        // 获取模型的 Renderer 组件
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // 修改材质的颜色为黄色
            renderer.material.color = Color.black;

            Debug.Log($"{gameObject.name} color changed to black!");
        }

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
        if(beSelected == true){
            this.gameObject.transform.position = input.GetRightHandPosition().position+Offset;
        }
    }
}
