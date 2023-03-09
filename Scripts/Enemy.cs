using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //存储敌人的各种数值以及敌人生命值的判定
    //用协程控制，每次攻击过后暂停0.5秒
    
    public int enemyHealth = 100;//敌人血量
    public int enemyAttack = 15;//敌人攻击
    public int enemyDefense = 10;//敌人防御
    public int enemyGold = 5;//战胜后获得的金币数量
    public int enemyExp = 10;//战胜后获得的经验值
    public Sprite enemyImage;

    private void Awake()
    {
        enemyImage = GetComponent<SpriteRenderer>().sprite;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        //PlayerUIController playerUIController = other.GetComponent<PlayerUIController>();
        
        if (playerController != null)
        {
            if(playerController.playerAttack > enemyDefense)
            {
                //playerController.isFighting = true;
                playerController.FightRoundStart(enemyHealth, enemyAttack, enemyDefense, enemyGold, enemyExp, enemyImage,gameObject);
                playerController.StartCoroutine(playerController.Fighting());
            }
        }
    }

}