using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixGuitar : MonoBehaviour
{
    public Transform skullHead; // 父对象的引用
    public GuitarFollowInMirror mopGuitar;
    [SerializeField] Vector3 FixPointOffset;
    private float distance; // 子对象与父对象的固定距离

    [SerializeField]private Vector3 initialOffset;

    void Start()
    {
        if (mopGuitar == null)
        {
            Debug.LogError("mopGuitar is empty for fixPoint！");
            return;
        }
        

        // 计算初始偏移
        initialOffset = transform.position - skullHead.position+ FixPointOffset;
        // initialOffset.y = 0; // 确保偏移仅在水平面上生效
        distance = Vector3.Distance(transform.position,skullHead.position);
    }

    void Update()
    {
        if (skullHead == null) return;

        // 获取父对象的Y轴旋转
        Quaternion parentRotation = Quaternion.Euler(0, skullHead.eulerAngles.y, 0);

        // 计算子对象的新位置
        //Vector3 newPosition = skullHead.position + parentRotation * initialOffset.normalized * distance;
        Vector3 newPosition = skullHead.position;
        // 保持高度不变
        //newPosition.y = skullHead.position.y + initialOffset.y;
        newPosition.y = skullHead.position.y;

        // 设置子对象位置
        transform.position = newPosition;
        transform.rotation = parentRotation;

        // 如果需要，子对象可以保持原本的朝向
        // transform.rotation = Quaternion.identity;
    }

}
