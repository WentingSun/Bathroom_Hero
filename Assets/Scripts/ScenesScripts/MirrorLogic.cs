using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorLogic : MonoBehaviour
{
    [SerializeField] private Camera CameraA;
    [SerializeField] private Camera CameraB;
    [SerializeField] private Material MirrorMaterial; // 使用 ScreenCutoutShader 的材质
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

    void Start()
    {
        // 确保镜子一开始有默认纹理
        MirrorMaterial.SetTexture("_MainTex", TextureMop);
    }
}

