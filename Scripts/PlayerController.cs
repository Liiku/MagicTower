using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    //�������
    [Header("�������")]
    public int playerHealth = 100;//���Ѫ��
    public int playerAttack = 15;//����
    public int playerDefense = 10;//����
    public int playerGold = 0;//���
    public int playerExp = 0;//����
    public int playerLvl = 1;//�ȼ�
    //Կ��
    [Header("���Կ��")]
    public int yellowKey = 1;//��ʼ��һ����Կ��
    public int violetKey = 1;//��ʼ��һ����Կ��
    public int redKey = 1;//��ʼ��һ����Կ��

    private PlayerUIController PlayerUIController;
    private Animator Animator;
    private AudioSource AudioSource;
    public CameraController CameraController;

    //ս��ʱ�ĵ�������
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

    //��Ҳ���
    [Header("��Ҳ���")]
    public float playerSpeed;//�ƶ��ٶ�
    public bool isWalk;
    public bool canWalk = true;
    public bool isShopping = false;
    public bool isPause = false;

    //Floor
    public int Floor = 1;
    public bool floor_fristOnTrigger = true;//����״ν���
    public bool floor_startCountDown = false;//�Ƿ�ʼ����ʱ
    public float floor_CountDown = 1.25f;//����¥ֹͣ����ƶ�ʱ��
    //�̵�ѡ�����
    public int currentOption = 0;//��ǰѡ��//ѡ���ֵΪ0��1��2������ѡ��
    int optionNumber = 2;//�ܹ�3��ѡ��

    SpriteRenderer SpriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerUIController = GetComponent<PlayerUIController>();
        Animator = GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();
        SpriteRenderer = GetComponent<SpriteRenderer>();//ͼ�����ҷ�ת
    }

    // Update is called once per frame
    void Update()
    {
        //�����ƶ�����
        Animator.SetBool("walk",isWalk);

        //ս����ʤ
        if (playerWin)
        {
            PlayerUIController.FightingUI.SetActive(false);
        }

        //���ڹ���
        if (isShopping)
        {
            canWalk = false;
            if (Input.GetKeyDown(KeyCode.S))//����Ұ���s��ѡ����һ��
            {
                currentOption++;
                if (currentOption > optionNumber)//ѡ�����һ��ѡ����ٴΰ�����ص���һ��ѡ��
                {
                    currentOption = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                currentOption--;
                if (currentOption < 0)//ѡ���һ��ѡ����ٴΰ���w���������һ��ѡ��
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
            if (Input.GetKeyDown(KeyCode.E))//�ر��̵�ui
            {
                isShopping = false;
                canWalk = true;
            }
        }
        
        //��Ϸ��ͣ
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
        }

        //����¥ʱֹͣ����ƶ�
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
        //��ɫ�ƶ�
        if (Input.GetKey(KeyCode.W) && canWalk)
        {
            transform.Translate(Vector2.up * Time.fixedDeltaTime * playerSpeed);
            isWalk = true;
        }
        else
        {
            isWalk = false;//?ֻд��һ��������ͨ��
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

    //����ֵ����
    public void HealthManage(int _hp)
    {
        playerHealth += _hp;
        if (_hp > 0)
        {
            PlayerUIController.IncreaseHealtText(_hp);
        }
    }
    
    //����������
    public void AttackManage(int _atk)
    {
        playerAttack += _atk;
        PlayerUIController.IncreaseAttackText(_atk);
    }

    //����������
    public void DefenseManage(int _def)
    {
        playerDefense += _def;
        PlayerUIController.IncreaseDefenseText(_def);
    }

    //��ҹ���//�������
    public void GoldExpManage(int _gold, int _exp)
    {
        playerGold += _gold;
        playerExp += _exp;
        PlayerUIController.FightEndText(_gold, _exp);
    }

    //Կ�׹���
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

    //��������
    public void PlaySound(AudioClip clip)
    {
        AudioSource.PlayOneShot(clip);
    }

    //����¥����
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

    //ս������
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
            //�����ܵ��˺�
            yield return new WaitForSeconds(roundSpeed);
            int enemyHurt = playerAttack - enemyDef;//�����ܵ��˺�=��ҹ���-�������
            enemyHp -= enemyHurt;//����ʣ������ֵ
            //����ܵ��˺�
            if (playerDefense < enemyAtk)
            {
                enemyDamage = enemyAtk - playerDefense;//��������˺�=���﹥��-��ҷ��� 
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
