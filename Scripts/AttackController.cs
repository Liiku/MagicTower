using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public int increaseAttackNumber = 15;
    void OnTriggerEnter2D(Collider2D other)
    {
        //���ӹ�����
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)//�ж��Ƿ�Ϊ���
        {
            playerController.AttackManage(increaseAttackNumber);//������ҵ����ӹ���������
            Destroy(gameObject);//�ݻ��Լ�
        }
    }
}
