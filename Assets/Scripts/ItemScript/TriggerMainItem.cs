using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

using UnityEngine.XR;


public class TriggerMainItem : MonoBehaviour
{
    private XRController controller; // 引用手柄控制器
    public BaseMusicItem musicItem; // 引用 TestMusicItem 脚本
    private bool isSelected = false; // 用于检测按键状态
    private bool isCoolingDown = false; // 冷却状态标记
    [SerializeField] private float coolDownTime = 1f; // 冷却时间（秒）
    [SerializeField]private InputDevice RightHandController; // 用于存储左手控制器的输入设备


    // Start is called before the first frame update
     void Awake()
    {
        // 获取当前手柄（例如左手或右手）
        controller = GetComponent<XRController>();
        // if (controller == null)
        // {
        //     Debug.LogError("XRController is not assigned to this object!");
        // }

    }


    // Update is called once per frame
    void Update()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);//
        Debug.Log(devices == null);
        Debug.Log(devices.Count);
        if (devices.Count  > 0 &&  devices != null)
        {
            RightHandController = devices[0];
            Debug.Log("Right-hand controller found!");
        }
        else
        {
            Debug.LogError("No Right-hand controller detected!");
        }
        // if (controller != null)
        // {
        //     // 检测 Trigger 按键是否被按下
        //     bool v = controller.inputDevice.TryGetFeatureValue(CommonUsages.PrimaryTrigger, out bool isPressed);

        //     if (isPressed && !isSelected) // 第一次按下
        //     {
        //         isSelected = true;

        //         if (musicItem != null)
        //         {
        //             musicItem.BeSelected();
        //             Debug.Log("Triggered BeSelected on MapB!");
        //         }
        //     }
        //     else if (!isPressed && isSelected) // 松开时重置状态
        //     {
        //         isSelected = false;
        //     }
        // }
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
