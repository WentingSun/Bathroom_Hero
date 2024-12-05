using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarFollowInMirror : MonoBehaviour
{
    [SerializeField] private Transform Mop;         // 需要调整位置的物体
    [SerializeField] private Transform FixPoint;    // 固定点
    [SerializeField] private Transform rightHandCollider; // 右手碰撞器
    public Vector3 rotationOffset;                 // 旋转的额外偏移
    public Transform skullLeftHand;
    public Transform skullRightHand;

    public Transform skullHead;

    private void Start()
    {
        StartCoroutine(UpdatePositionAndRotation());
    }

    private IEnumerator UpdatePositionAndRotation()
    {
        while (true)
        {
            Vector3 direction = skullLeftHand.position - FixPoint.position;
            Mop.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(rotationOffset);
            Mop.position = FixPoint.position;
            yield return null;
            // // 将 Mop 位置同步到 FixPoint
            // Mop.position = FixPoint.position;

            // // 设置 Mop 的朝向为左手位置
            // Vector3 direction = skullLeftHand.position - FixPoint.position;
            // Mop.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(rotationOffset);

            // // 同步右手碰撞器的位置
            // rightHandCollider.position = skullRightHand.position;

            // yield return null;

            


        }
    }

}
