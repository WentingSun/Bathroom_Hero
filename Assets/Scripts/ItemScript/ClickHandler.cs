using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{

    public TestMusicItem testMusicItem; // 引用 TestMusicItem 脚本
    private XRGrabInteractable grabInteractable;

    private bool isSelected = false; // 记录当前状态

    void Awake()
    {
        // 获取 XRGrabInteractable 组件
        grabInteractable = GetComponent<XRGrabInteractable>();

        // 确保 testMusicItem 已经设置
        if (testMusicItem == null)
        {
            testMusicItem = GetComponent<TestMusicItem>();
        }

        // 监听 XRGrabInteractable 的事件
        if (grabInteractable != null)
        {
            grabInteractable.onSelectEntered.AddListener(OnGripPressed);
        }
    }

    // 当按下 Grip 按钮并抓取物体时调用
    private void OnGripPressed(XRBaseInteractor interactor)
    {
        if (isSelected)
        {
            // 当前已被选中，触发 UnSelected()
            Debug.Log("Grip pressed again, deselecting model!");
            if (testMusicItem != null)
            {
                testMusicItem.UnSelected();
            }
            isSelected = false; // 切换到未选中状态
        }
        else
        {
            // 当前未被选中，触发 BeSelected()
            Debug.Log("Grip pressed, selecting model!");
            if (testMusicItem != null)
            {
                testMusicItem.BeSelected();
            }
            isSelected = true; // 切换到选中状态
        }
    }

}
