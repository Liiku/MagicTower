using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    //存储玩家的各种数值以及玩家生命值判定


    public int playerHealth = 100;//最大血量
    public int playerDefense = 10;//防御
    public int playerAttack = 15;//攻击
    public int playerGold = 0;//金币
    public int playerExp = 0;//经验
    public int playerLvl = 1;//等级

    public int blueKey = 1;//初始有一个蓝钥匙
    public int violetKey = 1;//初始有一个紫钥匙
    public int redKey = 1;//初始有一个红钥匙

    private PlayerInput PlayerInput;
    private PlayerUIController PlayerUIController;
    private Animator Animator;
    private AudioSource AudioSource;

    public bool isFighting = false;
    private float roundSpeed = 1.0f;
    public int enemyHp;
    public int enemyAtk;
    public int enemyDef;
    public int enemyGold;
    public int enemyExp;
    private GameObject enemyObject;
    public Sprite enemyImage;
    public bool playerWin = false;


    // Start is called before the first frame update
    void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        PlayerUIController = GetComponent<PlayerUIController>();
        Animator = GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //播放移动动画
        Animator.SetBool("walk", PlayerInput.isWalk);

        if (playerWin)
        {
            PlayerUIController.FightingUI.SetActive(false);
        }
        
 

    }
   
    //生命值管理
    public void HealthManage(int _hp)
    {
        playerHealth += _hp;
        if (_hp > 0)
        {
            PlayerUIController.IncreaseHealtText(_hp);
        }
    }
    
    //攻击力管理
    public void AttackManage(int _atk)
    {
        playerAttack += _atk;
        PlayerUIController.IncreaseAttackText(_atk);
    }

    //防御力管理
    public void DefenseManage(int _def)
    {
        playerDefense += _def;
        PlayerUIController.IncreaseDefenseText(_def);
    }

    //金币管理//经验管理
    public void GoldExpManage(int _gold, int _exp)
    {
        playerGold += _gold;
        playerExp += _exp;
        PlayerUIController.FightEndText(_gold, _exp);
    }

    //钥匙管理
    public void KeyManage(int _KeyNumber,string _KeyType)
    {
        if (_KeyType == "BLUE")
        {
            blueKey += _KeyNumber;
        }if (_KeyType == "VIOLET")
        {
            violetKey += _KeyNumber;
        }if (_KeyType == "RED")
        {
            redKey += _KeyNumber;
        }
        PlayerUIController.KeyManageText(_KeyNumber, _KeyType);
    }

    //声音管理
    public void PlaySound(AudioClip clip)
    {
        AudioSource.PlayOneShot(clip);
    }

    //战斗管理
    public void FightRoundStart(int e_hp,int e_atk,int e_def,int e_gold,int e_exp,GameObject e_enemy,Sprite e_enemyimage)
    {
        isFighting = true;
        enemyHp = e_hp;
        enemyAtk = e_atk;
        enemyDef = e_def;
        enemyObject = e_enemy;
        enemyGold = e_gold;
        enemyExp = e_exp;
        enemyImage = e_enemyimage;

        PlayerInput.canWalk = false;
    }
    public IEnumerator Fighting()
    {
        while (enemyHp > 0 && playerHealth > 0)
        {
            //敌人受到伤害
            int enemyHurt = playerAttack - enemyDef;//怪物受到伤害=玩家攻击-怪物防御
            enemyHp -= enemyHurt;//敌人剩余生命值
            //玩家受到伤害
            int enemyDamage = enemyAtk - playerDefense;//怪物造成伤害=怪物攻击-玩家防御
            HealthManage(-enemyDamage);
            Debug.Log("一回合结束，玩家造成" + enemyHurt + "伤害\n敌人造成" + enemyDamage + "伤害");
            if (enemyHp > 0 && playerHealth > 0)
            {
                yield return new WaitForSeconds(roundSpeed);
            }
        }
        isFighting = false;
        if (enemyHp <= 0 || playerHealth > 0)
        {
            Debug.Log("玩家胜利！");
            playerWin = true;
            GoldExpManage(enemyGold, enemyExp);
            Destroy(enemyObject);
        }
        if (playerHealth <= 0)
        {
            Debug.Log("玩家死亡！");
            PlayerInput.canWalk = false;
            PlayerUIController._DeadUI();
        }
    }


}
