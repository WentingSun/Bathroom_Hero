using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPointMoment : MonoBehaviour
{
    private Transform parent; // 父对象的引用
    public MopGuitar mopGuitar;
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
        parent = mopGuitar.input.GetHeadPosition();

        // 计算初始偏移
        initialOffset = transform.position - parent.position+ FixPointOffset;
        // initialOffset.y = 0; // 确保偏移仅在水平面上生效
        distance = Vector3.Distance(transform.position,parent.position);
    }

    void Update()
    {
        if (parent == null) return;

        // 获取父对象的Y轴旋转
        Quaternion parentRotation = Quaternion.Euler(0, parent.eulerAngles.y, 0);

        // 计算子对象的新位置
        Vector3 newPosition = parent.position + parentRotation * initialOffset.normalized * distance;

        // 保持高度不变
        newPosition.y = parent.position.y + initialOffset.y;

        // 设置子对象位置
        transform.position = newPosition;
        transform.rotation = parentRotation;

        // 如果需要，子对象可以保持原本的朝向
        // transform.rotation = Quaternion.identity;
    }
}
