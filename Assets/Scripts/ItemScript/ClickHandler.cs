using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{

    public TestMusicItem MusicItem; // 引用 MusicItem 脚本
    
    [SerializeField]private XRGrabInteractable grabInteractable;

    //测试用
    private Vector3 initialPosition; // 用于存储模型的初始位置
    private bool beSelected = false; // 记录当前模型是否吸附到手柄上
    private XRBaseInteractor currentInteractor; // 保存当前的手柄交互器


    void Awake()
    {
        // 获取 XRGrabInteractable 组件
        grabInteractable = GetComponent<XRGrabInteractable>();
        //测试用 记录初始位置
        initialPosition=this.transform.position;
        beSelected = false;

        if (grabInteractable == null)
        {
            grabInteractable = GetComponent<XRGrabInteractable>();
        }

        // 确保 MusicItem 已经设置
        if (MusicItem == null)
        {
            MusicItem = GetComponent<TestMusicItem>();
        }

        // 监听 XRGrabInteractable 的事件
        if (grabInteractable != null)
        {
            grabInteractable.onSelectEntered.AddListener(OnGripPressed);
        }
    }
    
    // public void setMusicItem(BaseMusicItem item){
    //     MusicItem = item;
    // }

    // 当按下 Grip 按钮并抓取物体时调用
    private void OnGripPressed(XRBaseInteractor interactor)
    {
        if (MusicItem != null)
        {
            // 如果模型当前未选中，调用 BeSelected()
            if (!beSelected) 
            {
                Debug.Log("Grip pressed, selecting model!");
                MusicItem.BeSelected();
                TestBeSelected();

                GameManager.Instance.UpdatePlayerState(PlayerState.playerSelectMop);
            }
            else 
            {
                // 如果模型已选中，调用 UnSelected()
                Debug.Log("Grip pressed again, deselecting model!");
                MusicItem.UnSelected();
                TestUnSelected();
                
                GameManager.Instance.UpdatePlayerState(PlayerState.PlayerSelectNothing);
            }

        }
    }

     public void TestBeSelected()
    {
        
        // 获取模型的 Renderer 组件
        Renderer renderer = GetComponent<Renderer>();
        // 修改材质的颜色为黄色
        renderer.material.color = Color.yellow;
        Debug.Log($"{gameObject.name} color changed to yellow!");
    }

    public  void TestUnSelected()
    {

        // 获取模型的 Renderer 组件
        Renderer renderer = GetComponent<Renderer>();
        // 修改材质的颜色为黑色
        renderer.material.color = Color.black;
        Debug.Log($"{gameObject.name} color changed to black!");

    }

}
