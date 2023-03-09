using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseStone : MonoBehaviour
{
    public int increaseDefenseNumber = 15;
    void OnTriggerEnter2D(Collider2D other)
    {
        //���ӷ�����
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)//�ж��Ƿ�Ϊ���
        {
            playerController.DefenseManage(increaseDefenseNumber);//������ҵ����ӷ���������
            Destroy(gameObject);//�ݻ��Լ�
        }
    }
}
