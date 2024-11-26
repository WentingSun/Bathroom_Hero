using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorLogic : MonoBehaviour
{
    [SerializeField] private Camera CameraA;
    [SerializeField] private Camera CameraB;
    [SerializeField] private Camera CameraC;
    [SerializeField] private Transform renderplaneB;
    [SerializeField] private Transform renderplaneC;
    [SerializeField] private Transform transformdoorB;
    [SerializeField] private Transform transformdoorC;
    [SerializeField] private Material MirrorMaterial; // 请确保你有 MirrorMaterial 字段
    [SerializeField] private Texture TextureMop;      // 拖把模式的纹理
    [SerializeField] private Texture TextureLightsaber; // 光剑模式的纹理

//
    [SerializeField] private Renderer renderplaneARenderer; // Add Renderer reference for renderplaneA
    [SerializeField] private Renderer renderplaneBRenderer; // Add Renderer reference for renderplaneB
    [SerializeField] private Renderer renderplaneCRenderer; // Add Renderer reference for renderplaneC
    void Awake()
    {
        GameManager.OnPlayerStateChage += ChangeScenesOnPlayerStateChange;
    }

    void OnDestroy()
    {
        GameManager.OnPlayerStateChage -= ChangeScenesOnPlayerStateChange;
    }

    void Start()
    {
        // 确保镜子一开始有默认纹理
        //MirrorMaterial.SetTexture("_MainTex", TextureMop);
        ToggleRenderPlaneVisibility(renderplaneARenderer);
        ToggleRenderPlaneUnVisibility(renderplaneBRenderer);
        ToggleRenderPlaneUnVisibility(renderplaneCRenderer);
    }

    public void ChangeScenesOnPlayerStateChange(PlayerState state)
    {
        Debug.Log("ChangeScenesOnPlayerStateChange");
        // 根据玩家状态，切换镜子的渲染逻辑
        Debug.Log(state);
        switch (state)
        {
            case PlayerState.playerSelectMop:
                // 设置材质的主纹理为拖把模式纹理
                // MirrorMaterial.SetTexture("_MainTex", TextureMop);
                ToggleRenderPlaneVisibility(renderplaneBRenderer);
                //ToggleRenderPlaneUnVisibility(renderplaneARenderer);
                ToggleRenderPlaneUnVisibility(renderplaneCRenderer);
                Debug.Log("MirrorLogic: Mop Texture Mode");
                //在此添加代码
                break;

            case PlayerState.playerSelectTubelight:
                // 设置材质的主纹理为光剑模式纹理
                // MirrorMaterial.SetTexture("_MainTex", TextureLightsaber);
                ToggleRenderPlaneVisibility(renderplaneCRenderer);
                //ToggleRenderPlaneUnVisibility(renderplaneARenderer);
                ToggleRenderPlaneUnVisibility(renderplaneBRenderer);
                Debug.Log("MirrorLogic: Tubelight Texture Mode");
                //在此添加代码
                break;

            case PlayerState.PlayerSelectNothing:
                Debug.Log("默认状态");
                //ToggleRenderPlaneVisibility(renderplaneARenderer);
                ToggleRenderPlaneUnVisibility(renderplaneBRenderer);
                ToggleRenderPlaneUnVisibility(renderplaneCRenderer);
                break;

            default:
                Debug.LogWarning("Unhandled PlayerState: " + state);
                
                //在此添加代码
                break;
        }
    }

     public void ToggleRenderPlaneVisibility(Renderer renderName)
    {
        renderName.enabled =true;
    }
    public void ToggleRenderPlaneUnVisibility(Renderer renderName)
    {
        renderName.enabled = false;
    }
    void Update()
    {
        // 确保 CameraB 的位置与 CameraA 同步
        if (CameraA != null && CameraB != null)
        {
            // 计算 CameraA 和 renderplaneB 之间的相对位置差
            Vector3 offset = CameraA.transform.position - renderplaneB.transform.position;

            // 更新 CameraB 的位置，使其与 transformdoorB 保持正确的相对位置
            CameraB.transform.position = transformdoorB.transform.position + offset;

            // 同步 CameraB 的旋转
            CameraB.transform.rotation = CameraA.transform.rotation;
        }

        // 确保 CameraB 的位置与 CameraA 同步
        if (CameraA != null && CameraC != null)
        {
            // 计算 CameraA 和 renderplaneC 之间的相对位置差
            Vector3 offset = CameraA.transform.position - renderplaneC.transform.position;

            // 更新 CameraC 的位置，使其与 transformdoorC 保持正确的相对位置
            CameraC.transform.position = transformdoorC.transform.position + offset;

            // 同步 CameraB 的旋转
            CameraC.transform.rotation = CameraA.transform.rotation;
        }
        
    }
}



