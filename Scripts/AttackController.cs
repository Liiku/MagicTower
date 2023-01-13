using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public int increaseAttackNumber = 15;
    void OnTriggerEnter2D(Collider2D other)
    {
        //增加攻击力
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)//判断是否为玩家
        {
            playerController.AttackManage(increaseAttackNumber);//调用玩家的增加攻击力函数
            Destroy(gameObject);//摧毁自己
        }
    }
}
