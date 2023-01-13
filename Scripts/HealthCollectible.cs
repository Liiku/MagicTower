using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int increaseHealthNumber = 100;
    void OnTriggerEnter2D(Collider2D other)
    {
        //��������ֵ
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)//�ж��Ƿ�Ϊ���
        {
            playerController.HealthManage(increaseHealthNumber);//������ҵ���������ֵ����
            Destroy(gameObject);//�ݻ��Լ�
        }
       //Debug.Log("Object that entered the trigger : " + other);
    }
}
