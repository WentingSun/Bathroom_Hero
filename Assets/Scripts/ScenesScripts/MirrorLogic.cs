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
        MirrorMaterial.SetTexture("_MainTex", TextureMop);
    }

    public void ChangeScenesOnPlayerStateChange(PlayerState state)
    {
        // 根据玩家状态，切换镜子的渲染逻辑
        switch (state)
        {
            case PlayerState.playerSelectMop:
                // 设置材质的主纹理为拖把模式纹理
                MirrorMaterial.SetTexture("_MainTex", TextureMop);
                break;

            case PlayerState.playerSelectTubelight:
                // 设置材质的主纹理为光剑模式纹理
                MirrorMaterial.SetTexture("_MainTex", TextureLightsaber);
                break;

            default:
                Debug.LogWarning("Unhandled PlayerState: " + state);
                break;
        }
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
    }
}


