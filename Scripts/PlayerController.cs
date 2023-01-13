using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    //�洢��ҵĸ�����ֵ�Լ��������ֵ�ж�


    public int playerHealth = 100;//���Ѫ��
    public int playerDefense = 10;//����
    public int playerAttack = 15;//����
    public int playerGold = 0;//���
    public int playerExp = 0;//����
    public int playerLvl = 1;//�ȼ�

    public int blueKey = 1;//��ʼ��һ����Կ��
    public int violetKey = 1;//��ʼ��һ����Կ��
    public int redKey = 1;//��ʼ��һ����Կ��

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
        //�����ƶ�����
        Animator.SetBool("walk", PlayerInput.isWalk);

        if (playerWin)
        {
            PlayerUIController.FightingUI.SetActive(false);
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

    //��������
    public void PlaySound(AudioClip clip)
    {
        AudioSource.PlayOneShot(clip);
    }

    //ս������
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
            //�����ܵ��˺�
            int enemyHurt = playerAttack - enemyDef;//�����ܵ��˺�=��ҹ���-�������
            enemyHp -= enemyHurt;//����ʣ������ֵ
            //����ܵ��˺�
            int enemyDamage = enemyAtk - playerDefense;//��������˺�=���﹥��-��ҷ���
            HealthManage(-enemyDamage);
            Debug.Log("һ�غϽ�����������" + enemyHurt + "�˺�\n�������" + enemyDamage + "�˺�");
            if (enemyHp > 0 && playerHealth > 0)
            {
                yield return new WaitForSeconds(roundSpeed);
            }
        }
        isFighting = false;
        if (enemyHp <= 0 || playerHealth > 0)
        {
            Debug.Log("���ʤ����");
            playerWin = true;
            GoldExpManage(enemyGold, enemyExp);
            Destroy(enemyObject);
        }
        if (playerHealth <= 0)
        {
            Debug.Log("���������");
            PlayerInput.canWalk = false;
            PlayerUIController._DeadUI();
        }
    }


}
