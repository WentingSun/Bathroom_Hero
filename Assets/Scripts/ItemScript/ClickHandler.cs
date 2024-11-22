using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{

    public BaseMusicItem MusicItem; // 引用 TestMusicItem 脚本
    [SerializeField]private XRGrabInteractable grabInteractable;


    void Awake()
    {
        // 获取 XRGrabInteractable 组件
        grabInteractable = GetComponent<XRGrabInteractable>();

        // 确保 testMusicItem 已经设置
        if (MusicItem == null)
        {
            MusicItem = GetComponent<BaseMusicItem>();
        }

        // 监听 XRGrabInteractable 的事件
        if (grabInteractable != null)
        {
            grabInteractable.onSelectEntered.AddListener(OnGripPressed);
        }
    }
    
    public void setMusicItem(BaseMusicItem item){
        MusicItem = item;
    }

    // 当按下 Grip 按钮并抓取物体时调用
    private void OnGripPressed(XRBaseInteractor interactor)
    {
        if (MusicItem != null)
        {
            // 如果模型当前未选中，调用 BeSelected()
            if (!MusicItem.beSelected) 
            {
                Debug.Log("Grip pressed, selecting model!");
                MusicItem.BeSelected();
                GameManager.Instance.UpdatePlayerState(PlayerState.playerSelectMop);
            }
            else 
            {
                // 如果模型已选中，调用 UnSelected()
                Debug.Log("Grip pressed again, deselecting model!");
                MusicItem.UnSelected();
                GameManager.Instance.UpdatePlayerState(PlayerState.PlayerSelectNothing);
            }

        }
    }

}
