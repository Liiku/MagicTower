using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseStone : MonoBehaviour
{
    public int increaseDefenseNumber = 15;
    void OnTriggerEnter2D(Collider2D other)
    {
        //增加防御力
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)//判断是否为玩家
        {
            playerController.DefenseManage(increaseDefenseNumber);//调用玩家的增加防御力函数
            Destroy(gameObject);//摧毁自己
        }
    }
}
