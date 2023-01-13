using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public string keyType = "BLUE";
    //public int keyNumber = 1; ????????为什么不可以????

    //管理钥匙
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)//判断是否为玩家
        {
            playerController.KeyManage(1,keyType);//调用玩家的钥匙管理函数
            Destroy(gameObject);
        }
    }
}
