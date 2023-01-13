using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //�洢���˵ĸ�����ֵ�Լ���������ֵ���ж�
    //��Э�̿��ƣ�ÿ�ι���������ͣ0.5��
    
    public int enemyHealth = 100;//����Ѫ��
    public int enemyDefense = 10;//���˷���
    public int enemyAttack = 15;//���˹���
    public int enemyGold = 5;//սʤ���õĽ������
    public int enemyExp = 10;//սʤ���õľ���ֵ
    public Sprite enemyImage;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        //PlayerUIController playerUIController = other.GetComponent<PlayerUIController>();

        if (playerController != null)
        {
            playerController.isFighting = true;
            playerController.FightRoundStart(enemyHealth, enemyAttack, enemyDefense,enemyGold,enemyExp,gameObject,enemyImage);
            playerController.StartCoroutine(playerController.Fighting());
        }
    }
}