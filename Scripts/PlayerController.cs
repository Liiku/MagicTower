using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    //玩家属性
    [Header("玩家属性")]
    public int playerHealth = 100;//最大血量
    public int playerAttack = 15;//攻击
    public int playerDefense = 10;//防御
    public int playerGold = 0;//金币
    public int playerExp = 0;//经验
    public int playerLvl = 1;//等级
    //钥匙
    [Header("玩家钥匙")]
    public int yellowKey = 1;//初始有一个蓝钥匙
    public int violetKey = 1;//初始有一个紫钥匙
    public int redKey = 1;//初始有一个红钥匙

    private PlayerUIController PlayerUIController;
    private Animator Animator;
    private AudioSource AudioSource;
    public CameraController CameraController;

    //战斗时的敌人属性
    public bool isFighting = false;
    public int enemyHp;
    public int enemyAtk;
    public int enemyDef;
    public int enemyGold;
    public int enemyExp;
    public Sprite enemyImage;
    private GameObject enemyObject;
    public bool playerWin = false;
    private float roundSpeed = 0.7f;

    //玩家操作
    [Header("玩家操作")]
    public float playerSpeed;//移动速度
    public bool isWalk;
    public bool canWalk = true;
    public bool isShopping = false;
    public bool isPause = false;

    //Floor
    public int Floor = 1;
    public bool floor_fristOnTrigger = true;//玩家首次进入
    public bool floor_startCountDown = false;//是否开始倒计时
    public float floor_CountDown = 1.25f;//上下楼停止玩家移动时间
    //商店选项控制
    public int currentOption = 0;//当前选项//选项的值为0，1，2的三个选项
    int optionNumber = 2;//总共3个选项

    SpriteRenderer SpriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerUIController = GetComponent<PlayerUIController>();
        Animator = GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();
        SpriteRenderer = GetComponent<SpriteRenderer>();//图像左右翻转
    }

    // Update is called once per frame
    void Update()
    {
        //播放移动动画
        Animator.SetBool("walk",isWalk);

        //战斗获胜
        if (playerWin)
        {
            PlayerUIController.FightingUI.SetActive(false);
        }

        //正在购物
        if (isShopping)
        {
            canWalk = false;
            if (Input.GetKeyDown(KeyCode.S))//当玩家按下s，选择下一项
            {
                currentOption++;
                if (currentOption > optionNumber)//选择最后一个选项后再次按下则回到第一个选项
                {
                    currentOption = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                currentOption--;
                if (currentOption < 0)//选择第一个选项后再次按下w则来到最后一个选项
                {
                    currentOption = optionNumber;
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (currentOption == 0)
                {
                    if(playerGold >= 25)
                    {
                        playerGold = playerGold - 25;
                        AttackManage(5);
                    }
                    else
                    {
                        PlayerUIController.NotEnoughGold();
                    }                   
                }
                else if (currentOption == 1)
                {
                    if (playerGold >= 25)
                    {
                        playerGold = playerGold - 25;
                        DefenseManage(5);
                    }
                    else
                    {
                        PlayerUIController.NotEnoughGold();
                    }
                }
                else
                {
                    if (playerGold >= 25)
                    {
                        playerGold = playerGold - 25;
                        HealthManage(250);
                    }
                    else
                    {
                        PlayerUIController.NotEnoughGold();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.E))//关闭商店ui
            {
                isShopping = false;
                canWalk = true;
            }
        }
        
        //游戏暂停
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
        }

        //上下楼时停止玩家移动
        if (floor_startCountDown)
        {
            floor_CountDown -= Time.deltaTime;
            if (floor_CountDown <= 0f)
            {
                canWalk = true;
                floor_CountDown = 1.25f;
                floor_startCountDown = false;
            }
            else
            {
                canWalk = false;
            }
        }
    }

    void FixedUpdate()
    {
        //角色移动
        if (Input.GetKey(KeyCode.W) && canWalk)
        {
            transform.Translate(Vector2.up * Time.fixedDeltaTime * playerSpeed);
            isWalk = true;
        }
        else
        {
            isWalk = false;//?只写了一个其他都通用
        }
        if (Input.GetKey(KeyCode.S) && canWalk)
        {
            transform.Translate(Vector2.down * Time.fixedDeltaTime * playerSpeed);
            isWalk = true;
        }
        if (Input.GetKey(KeyCode.A) && canWalk)
        {
            SpriteRenderer.flipX = true;
            transform.Translate(Vector2.left * Time.fixedDeltaTime * playerSpeed);
            isWalk = true;
        }
        if (Input.GetKey(KeyCode.D) && canWalk)
        {
            SpriteRenderer.flipX = false;
            transform.Translate(Vector2.right * Time.fixedDeltaTime * playerSpeed);
            isWalk = true;
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
        if (_KeyType == "YELLOW")
        {
            yellowKey += _KeyNumber;
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

    //上下楼管理
    public void UpFloor()
    {
        CameraController.Camera(true);
        Floor++;
        PlayerUIController.FloorText();
    }
    public void DownFloor()
    {
        CameraController.Camera(false);
        Floor--;
        PlayerUIController.FloorText();
    }

    //战斗管理
    public void FightRoundStart(int e_hp,int e_atk,int e_def,int e_gold,int e_exp,Sprite e_enemyimage,GameObject e_enemyObject)
    {
        isFighting = true;
        enemyHp = e_hp;
        enemyAtk = e_atk;
        enemyDef = e_def;
        enemyGold = e_gold;
        enemyExp = e_exp;
        enemyObject = e_enemyObject;
        enemyImage = e_enemyimage;

        canWalk = false;
    }
    public IEnumerator Fighting()
    {
        while (enemyHp > 0 && playerHealth > 0)
        {
            int enemyDamage = 0;
            //敌人受到伤害
            yield return new WaitForSeconds(roundSpeed);
            int enemyHurt = playerAttack - enemyDef;//怪物受到伤害=玩家攻击-怪物防御
            enemyHp -= enemyHurt;//敌人剩余生命值
            //玩家受到伤害
            if (playerDefense < enemyAtk)
            {
                enemyDamage = enemyAtk - playerDefense;//怪物造成伤害=怪物攻击-玩家防御 
            }
            else
            {
                enemyDamage = 0;
            }
            HealthManage(-enemyDamage);
            if (enemyHp <= 0)
            {
                enemyHp = 0;
                yield return new WaitForSeconds(roundSpeed);
            }
        }
        isFighting = false;
        if (enemyHp == 0 && playerHealth > 0)
        {
            playerWin = true;
            GoldExpManage(enemyGold, enemyExp);
            Destroy(enemyObject);
        }
        if (playerHealth <= 0)
        {
            canWalk = false;
            PlayerUIController._DeadUI();
        }
    }
}
