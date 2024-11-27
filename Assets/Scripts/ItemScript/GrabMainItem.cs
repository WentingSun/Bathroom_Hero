using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabMainItem : MonoBehaviour
{
    private XRGrabInteractable grabInteractable; // 引用 XR Grab Interactable
    [SerializeField] private BaseMusicItem currentMusicItem;     // 当前交互的 BaseMusicItem
    public BaseMusicItem realItem;
    public GameObject fakemop;
    public GameObject fakepipe;
    
    
    
    public bool MopBeSelected = false;
    public bool PipeBeSelected = false;
    public GameObject hidePoint;
    private Vector3 initialPosition; // 用于存储模型的初始位置
    public GameManager gameManager; // 引用 GameManager
    private bool isSelected = false;            // 跟踪当前是否已选中
    private float cooldown = 0.5f;              // 冷却时间（秒）
    private bool isCoolingDown = false;         // 冷却标志
    [SerializeField]private bool isHide= false;

    // 静态引用，用于全局管理 Mop 和 Pipe 的状态
    private static GrabMainItem currentlySelectedItem;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

         // 在脚本初始化时获取并存储模型的初始位置
        initialPosition = this.transform.position;


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
            selectedObject = realItem.gameObject;
            HandleSelection(selectedObject, "Mop");
            isHide =true;
            //MopBeSelected = true;
            GameManager.Instance.UpdatePlayerState(PlayerState.playerSelectMop);
        }
        else if (selectedObject.CompareTag("Pipe"))
        {
            selectedObject = realItem.gameObject;
            HandleSelection(selectedObject, "Pipe");
            isHide =true;
            //PipeBeSelected = true;
            GameManager.Instance.UpdatePlayerState(PlayerState.playerSelectTubelight);
        }

        // 开始冷却
        //StartCoroutine(CooldownCoroutine());
    }


    private void HandleSelection(GameObject selectedObject, string type)
    {
        // 如果有其他对象被选中，先取消其选中状态
        if (currentlySelectedItem != null && currentlySelectedItem != this)
        {
            currentlySelectedItem.HandleUnselection();
        }

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
            isSelected = true;
            currentlySelectedItem = this; // 更新当前选中项
        }
        else
        {
            HandleUnselection(); // 如果再次选中相同对象，则取消选中
        }
    }
    private void HandleUnselection()
    {
        if (isSelected && currentMusicItem != null)
        {
            Debug.Log($"Deselected {currentMusicItem.name}");
            currentMusicItem.UnSelected();
            isSelected = false;

            if (currentlySelectedItem == this)
            {
                currentlySelectedItem = null; // 重置当前选中项
            }
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

    void Update()
    {
        // 获取模型的 Renderer 组件
        Renderer renderer = GetComponent<Renderer>();
        //Renderer fakerenderer = fakeItem.GetComponent<Renderer>();
        if (isHide){
            this.gameObject.transform.position = initialPosition;
            renderer.enabled = false; // 隐藏模型
        }
        if(!isHide){
            renderer.enabled = true;
        }

        
    }

}
