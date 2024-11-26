using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class InputOnTrigger : MonoBehaviour
{

    public InputActionProperty triggerButton;
    public BaseMusicItem musicItem;
    public XRRayInteractor handRay; // 手柄射线组件
    public LineRenderer rayLineRenderer; // 用于显示射线的LineRenderer
    private Color defaultColor = Color.red; // 默认射线颜色
    private Color hitColor = Color.white;   // 命中目标后的射线颜色
    public string MopTarget = "Mop"; // 目标模型的Tag
    public string PipeTarget = "Pipe";
    // Start is called before the first frame update
    void Start()
    {
        if (rayLineRenderer != null)
        {
            rayLineRenderer.startColor = defaultColor;
            rayLineRenderer.endColor = defaultColor;
        }
        //Debug.Log(handRay == null);
        if (handRay == null)
        {
            // 尝试自动查找手柄上的 XRRayInteractor
            handRay = GameObject.Find("LefttHand Controller")?.GetComponent<XRRayInteractor>();
            Debug.LogError("Hand Ray Interactor could not be found! Please assign it in the Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float triggerPressValue = triggerButton.action.ReadValue<float>();
        CheckHandRayHit();

        if(triggerPressValue > 0.1)
        {
            OnTriggerPressed();
            Debug.Log("Trigger Pressed");
            //musicItem.BeSelected();
        }
    }
    // 检测手柄射线是否命中目标
    void CheckHandRayHit()
    {
        if (handRay == null || rayLineRenderer == null)
        {
            Debug.Log("Hand Ray Interactor is not assigned!");
            return;
        }
        // 声明一个 RaycastHit hit 变量，避免重复定义
        RaycastHit hit;

        // 尝试获取手柄射线的碰撞信息
        if (handRay.TryGetCurrent3DRaycastHit(out hit))
        {
            if (hit.collider.CompareTag(PipeTarget))
            {
            // 更改射线颜色为白色
                rayLineRenderer.startColor = hitColor;
                rayLineRenderer.endColor = hitColor;
                Debug.Log("Ray hit the target!");
                return;
            }
            // 如果没有命中，恢复默认颜色
            rayLineRenderer.startColor = defaultColor;
            rayLineRenderer.endColor = defaultColor;
        } 
    }
    void OnTriggerPressed()
    {
        // 重用 CheckHandRayHit 中的变量名
        RaycastHit hit;

        if (handRay.TryGetCurrent3DRaycastHit(out hit))
        {
            if (hit.collider.CompareTag(PipeTarget))
            {
                var target = hit.collider.GetComponent<BaseMusicItem>();
                if (target != null)
                {
                    target.BeSelected();
                    Debug.Log("Target Model A Selected via Hand Ray!");
                }
            }
        }
    }
}

