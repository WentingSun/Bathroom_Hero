using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorLogic : MonoBehaviour
{
    [SerializeField]private Camera CameraA; 
    [SerializeField]private Camera CameraB; 

    void Awake(){
        GameManager.OnPlayerStateChage+=ChangeScenesOnPlayerStateChange;
    }

    void OnDestroy(){
        GameManager.OnPlayerStateChage-=ChangeScenesOnPlayerStateChange;
    }

    public void ChangeScenesOnPlayerStateChange(PlayerState state){
        //当玩家状态变化(也就是他选择了拖把或者光剑), 镜子需要发生变化
        //更换镜子的渲染贴图所绑定的摄像机
        //TODO
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
