using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabMainItem : MonoBehaviour
{
    private XRGrabInteractable grabInteractable; // 引用 XR Grab Interactable
    private BaseMusicItem currentMusicItem;     // 当前交互的 BaseMusicItem
    public BaseMusicItem realMop; 
    public GameManager gameManager; // 引用 GameManager
    private bool isSelected = false;            // 跟踪当前是否已选中
    private float cooldown = 0.5f;              // 冷却时间（秒）
    private bool isCoolingDown = false;         // 冷却标志

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
        else
        {
            Debug.LogError("XRGrabInteractable component is missing!");
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (isCoolingDown) return; // 冷却中，忽略操作

        // 检测当前抓取的对象
        GameObject selectedObject = args.interactableObject.transform.gameObject;

        // 根据标签判断触发逻辑
        if (selectedObject.CompareTag("Mop"))
        {
            selectedObject = realMop.gameObject;
            HandleSelection(selectedObject, "Mop");
            GameManager.Instance.UpdatePlayerState(PlayerState.playerSelectMop);
        }
        else if (selectedObject.CompareTag("Pipe"))
        {
            HandleSelection(selectedObject, "Pipe");
            GameManager.Instance.UpdatePlayerState(PlayerState.playerSelectTubelight);
        }

        // 开始冷却
        StartCoroutine(CooldownCoroutine());
    }

    private void HandleSelection(GameObject selectedObject, string type)
    {
        // 获取 BaseMusicItem 脚本
        currentMusicItem = selectedObject.GetComponent<BaseMusicItem>();

        if (currentMusicItem == null)
        {
            Debug.LogWarning($"No BaseMusicItem script attached to {type}!");
            return;
        }

        if (!isSelected)
        {
            // 调用 BeSelected
            Debug.Log($"{type} selected!");
            currentMusicItem.BeSelected();
            //TestBeSelected();
            isSelected = true;
        }
        else
        {
            // 调用 UnSelected
            Debug.Log($"{type} deselected!");
            currentMusicItem.UnSelected();
            //TestUnSelected();
            isSelected = false;
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldown);
        isCoolingDown = false;
    }
    public void TestBeSelected()
    {
        // 获取模型的 Renderer 组件
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.yellow;
            //renderer.enabled = false; // 隐藏模型
            //this.transform.position = initialPosition;

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
