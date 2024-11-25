 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TriggerMainItem : MonoBehaviour
{
    private InputDevice leftHandController; // 用于存储左手控制器的输入设备
    public BaseMusicItem musicItem; // 引用 TestMusicItem 脚本
    private bool isSelected = false; // 记录当前是否选中状态
    private bool isCoolingDown = false; // 冷却状态标记
    [SerializeField] private float coolDownTime = 1f; // 冷却时间（秒）


    // Start is called before the first frame update
     void Awake()
    {
        // 获取左手手柄设备
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices);

        if (devices.Count > 0)
        {
            leftHandController = devices[0];
            Debug.Log("Left-hand controller found!");
        }
        else
        {
            Debug.LogError("No left-hand controller detected!");
        }
    }


    // Update is called once per frame
    void Update()
    {
        // 确保左手手柄存在
        if (leftHandController.isValid)
        {
            // 检查左手的 Trigger 键是否被按下
            if (leftHandController.TryGetFeatureValue(CommonUsages.triggerButton, out bool isPressed) && isPressed)
            {
                HandleTriggerPress();
            }
        }
    }
    private void HandleTriggerPress()
    {
        // 如果冷却中，直接返回
        if (isCoolingDown) return;

        if (musicItem != null)
        {
            if (!isSelected)
            {
                // 调用 BeSelected 函数
                musicItem.BeSelected();
                TestBeSelected();
                GameManager.Instance.UpdatePlayerState(PlayerState.playerSelectMop);
                Debug.Log("BeSelected called.");
            }
            else
            {
                // 调用 UnSelected 函数
                musicItem.UnSelected();
                TestUnSelected();
                GameManager.Instance.UpdatePlayerState(PlayerState.PlayerSelectNothing);
                Debug.Log("UnSelected called.");
            }

            // 切换状态
            isSelected = !isSelected;
            // 开启冷却
            StartCoroutine(CoolDown());
        }
        else
        {
            Debug.LogWarning("MapB is not set.");
        }
    }
    private IEnumerator CoolDown()
    {
        isCoolingDown = true; // 标记进入冷却状态
        yield return new WaitForSeconds(coolDownTime); // 等待冷却时间
        isCoolingDown = false; // 标记冷却结束
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
