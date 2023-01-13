using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int increaseHealthNumber = 100;
    void OnTriggerEnter2D(Collider2D other)
    {
        //增加生命值
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)//判断是否为玩家
        {
            playerController.HealthManage(increaseHealthNumber);//调用玩家的增加生命值函数
            Destroy(gameObject);//摧毁自己
        }
       //Debug.Log("Object that entered the trigger : " + other);
    }
}
