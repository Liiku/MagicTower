using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public string keyType = "BLUE";
    //public int keyNumber = 1; ????????Ϊʲô������????

    //����Կ��
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)//�ж��Ƿ�Ϊ���
        {
            playerController.KeyManage(1,keyType);//������ҵ�Կ�׹�����
            Destroy(gameObject);
        }
    }
}
