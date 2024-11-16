using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMove : MonoBehaviour
{
    public Camera mainCamera; // ������������ڽ���Ļ����ת��Ϊ��������
    public float zDistance = 10f; // ��������������

    void Update()
    {
        // ��ȡ�������Ļ�ϵ�λ��
        Vector3 mousePosition = Input.mousePosition;

        // ����Ļ����ת��Ϊ��������
        mousePosition.z = zDistance; // �������
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // �����ֲ�λ��Ϊ���λ��
        transform.position = worldPosition;
    }
}
