using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabMainItem : MonoBehaviour
{
    private XRGrabInteractable grabInteractable; // 引用 XR Grab Interactable
    public BaseMusicItem musicItem; // 引用 TestMusicItem 脚本
    private bool isSelected = false; // 记录当前是否选中状态

    void Awake()
    {
        // 获取 XRGrabInteractable 组件
        grabInteractable = GetComponent<XRGrabInteractable>();

        // 获取 TestMusicItem 组件
        musicItem = GetComponent<BaseMusicItem>();

        if (grabInteractable != null)
        {
            // 监听手柄的 Grab 动作（selectEntered）
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
    }

    // 当按下 Grab 键时调用
    private void OnGrab(SelectEnterEventArgs args)
    {
        if (musicItem != null)
        {
            if (!isSelected)
            {
                // 调用选中逻辑
                Debug.Log("Grab key pressed, selecting.");
                musicItem.BeSelected();
                TestBeSelected();
                isSelected = true;
            }
            else
            {
                // 调用取消选中逻辑
                Debug.Log("Grab key pressed again, deselecting.");
                musicItem.UnSelected();
                TestUnSelected();
                isSelected = false;
            }
        }
        else
        {
            Debug.LogWarning("TestMusicItem is not attached to this GameObject.");
        }
    }

    public void TestBeSelected()
    {
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
            Debug.LogWarning("Renderer component is missing!");
        }
    }

    public void TestUnSelected()
    {
        // 获取模型的 Renderer 组件
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // 修改材质的颜色为黑色
            renderer.material.color = Color.black;
            Debug.Log($"{gameObject.name} color changed to black!");
        }
        else
        {
            Debug.LogWarning("Renderer component is missing!");
        }
    }
    
}
