using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    //����Ҫ��õ���Ŀȫ������������ܽű���ʵ�ִ�������ã�
    //����Ҳ������ʵ��!
    [Header("playerUI")]
    public TextMeshProUGUI HP;
    public TextMeshProUGUI ATK;
    public TextMeshProUGUI DEF;
    public TextMeshProUGUI GOLD;
    public TextMeshProUGUI EXP;
    public TextMeshProUGUI LVL;
    public TextMeshProUGUI FT;
    public TextMeshProUGUI Yellowkey;
    public TextMeshProUGUI Violetkey;
    public TextMeshProUGUI Redkey;

    [Header("FightingUI")]
    public GameObject FightingUI;
    public TextMeshProUGUI P_HP;
    public TextMeshProUGUI P_ATK;
    public TextMeshProUGUI P_DEF;

    public TextMeshProUGUI E_HP;
    public TextMeshProUGUI E_ATK;
    public TextMeshProUGUI E_DEF;
    public Image E_Image;
    public GameObject E_Object;

    [Header("EnemyProperty")]
    public TextMeshProUGUI EP_HP;//�����������
    public TextMeshProUGUI EP_ATK;
    public TextMeshProUGUI EP_DEF;
    public TextMeshProUGUI EP_GOLD;
    public TextMeshProUGUI EP_EXP;
    public Image EP_image;
    public GameObject EP_enemyUI;
    public bool EP_UI;

    [Header("FloorUI")]
    public TextMeshProUGUI Floor;
    public GameObject FloorUI;

    //storeUI �̵�UI���ı�
    [Header("StoreUI")]
    public GameObject StoreUI;//�̵�UI
    public GameObject AttButton;
    public GameObject DefButton;
    public GameObject HpButton;
    //�����ı�
    [Header("����UI")]
    public TextMeshProUGUI textLabel;
    public Image faceImage;
    public GameObject talkUI;
    public bool fristTime;

    [Header("text")]
    //public TextAsset textFile;//��Ҫ��ʾ���ı�
    public bool isDialogue = false;
    public int index;
    public float textSpeed = 0.1f;//�ı��ٶ�
    public bool textFinished;//�Ƿ���ɴ���
    public bool cancelTyping;//�Ƿ�ȡ������

    public List<string> textList = new List<string>();//�½�һ�������б�

    PlayerController PlayerController;

    public GameObject FightEndUI;//ս����ʾ��
    private float textTimer = 2.0f;
    private bool isTimer = false;
    //��ͣui
    public GameObject PauseUI;
    //����ui
    public GameObject DeadUI;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //���
        HP.text = "HP:    " + PlayerController.playerHealth;//״̬��Ѫ��
        ATK.text = "ATK:    " + PlayerController.playerAttack;//״̬������
        DEF.text = "DEF:    " + PlayerController.playerDefense;//״̬������
        GOLD.text = "GOLD:    " + PlayerController.playerGold;//״̬�����
        EXP.text = "EXP:    " + PlayerController.playerExp;//״̬������
        LVL.text = "" + PlayerController.playerLvl;//״̬���ȼ�
        Yellowkey.text = "X " + PlayerController.yellowKey;//״̬����ɫԿ��
        Violetkey.text = "X " + PlayerController.violetKey;//״̬����ɫԿ��
        Redkey.text = "X " + PlayerController.redKey;//״̬����ɫԿ��
        //����
        if (PlayerController.isFighting)
        {
            FightingUI.SetActive(true);
            P_HP.text = "HP: " + PlayerController.playerHealth;//���״̬��Ѫ��
            P_ATK.text = "ATK: " + PlayerController.playerAttack;//���״̬������
            P_DEF.text = "DEF: " + PlayerController.playerDefense;//���״̬������

            E_HP.text = "HP: " + PlayerController.enemyHp;//����״̬��Ѫ��
            E_ATK.text = "ATK: " + PlayerController.enemyAtk;//����״̬������
            E_DEF.text = "DEF: " + PlayerController.enemyDef;//����״̬������
            E_Image.sprite = PlayerController.enemyImage;
           
        }

        


        //�ı���������ʱ
        if (isTimer)   
       {
          if(textTimer > 0)
          {
                textTimer -= Time.deltaTime;
          }
          else
          {
                FightEndUI.SetActive(false);
                FloorUI.SetActive(false);
                textTimer = 2;
                isTimer = false;
          }
       }
       //������ڹ���
       if (PlayerController.isShopping)
       {
           StoreManageText(PlayerController.currentOption);
       }
       else
       {
           StoreUI.SetActive(false);
        }
       //�����ͣ
       if (PlayerController.isPause)
       {
           PauseStart();
       }
       else
       {
           PauseEnd();
       }

        //�����ı�����ж�
        if (isDialogue)
        {
            if (Input.GetKeyDown(KeyCode.F) && index == textList.Count)//���Ž�����ر�ui
            {
                PlayerController.canWalk = true;
                talkUI.SetActive(false);
                isDialogue = false;
                index = 0;
                return;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (textFinished && !cancelTyping)
                {
                    StartCoroutine(SetTextUI());
                }
                else if (!textFinished)
                {
                    cancelTyping = !cancelTyping;
                }
            }
        }
        
    }

    //���������ʾ
    public void EnemyPropertyUI(int e_hp, int e_atk, int e_def, int e_gold, int e_exp, Sprite e_enemyimage)
    {
        EP_enemyUI.SetActive(EP_UI);
        EP_HP.text = "HP:    " + e_hp;//״̬��Ѫ��
        EP_ATK.text = "ATK:    " + e_atk;//״̬������
        EP_DEF.text = "DEF:    " + e_def;//״̬������
        EP_GOLD.text = "GOLD:    " + e_gold;//״̬�����
        EP_EXP.text = "EXP:    " + e_exp;//״̬������
        EP_image.sprite = e_enemyimage;
        isTimer = true;
    }
    

    //ս�������ı���ʾ
    public void FightEndText(int _Exp,int _Gold)
    {
        FightEndUI.SetActive(true);
        FT.text = "GET " + _Gold + " GOLD! GET " + _Exp + " EXP!";
        PlayerController.canWalk = true;
        isTimer = true;
    }

    //
    public void FloorText()
    {
        FloorUI.SetActive(true);
        Floor.text = "��" + PlayerController.Floor + "�A";
        isTimer = true;
    }

    //��������ֵ�ı���ʾ
    public void IncreaseHealtText(int _Hp)
    {
        FightEndUI.SetActive(true);
        FT.text = "Increase " + _Hp + " HP!";
        isTimer = true;
    }

    //���ӹ������ı���ʾ
    public void IncreaseAttackText(int _atk)
    {
        FightEndUI.SetActive(true);
        FT.text = "Increase " + _atk + " ATK!";
        isTimer = true;
    }
    
    //���ӷ������ı���ʾ
    public void IncreaseDefenseText(int _def)
    {
        FightEndUI.SetActive(true);
        FT.text = "Increase " + _def + " DEF!";
        isTimer = true;
    }

    //���Կ���ı���ʾ
    public void KeyManageText(int _KeyNumber,string _keyType)
    {
        FightEndUI.SetActive(true);
        if (_KeyNumber > 0)
        {
            FT.text = "GET " + _KeyNumber + " " + _keyType + "KEY!";
        }
        else
        {
            FT.text = "USE " + -_KeyNumber + " " + _keyType + "KEY!";
        }
        isTimer = true;
    }

    //��Ҳ���
    public void NotEnoughGold()
    {
        FightEndUI.SetActive(true);
        FT.text = "Not Enough Gold!";
        isTimer = true;
    }

    //�̵��ı���ʾ
    public void StoreManageText(int _numb)
    {
        StoreUI.SetActive(true);
        switch (_numb)//����ֵȷ����ǰ��ѡ��
        {
            case 0:
                AttButton.transform.GetComponent<Outline>().effectColor = new Color(245f,245f,245f,255f);//ѡ���
                DefButton.transform.GetComponent<Outline>().effectColor = new Color(245f,245f,245f,0f);
                HpButton.transform.GetComponent<Outline>().effectColor = new Color(245f,245f,245f,0f);
                break;
            case 1:
                AttButton.transform.GetComponent<Outline>().effectColor = new Color(245f, 245f, 245f, 0f);
                DefButton.transform.GetComponent<Outline>().effectColor = new Color(245f, 245f, 245f, 255f);//ѡ���
                HpButton.transform.GetComponent<Outline>().effectColor = new Color(245f, 245f, 245f, 0f);
                break;
            case 2:
                AttButton.transform.GetComponent<Outline>().effectColor = new Color(245f, 245f, 245f, 0f);
                DefButton.transform.GetComponent<Outline>().effectColor = new Color(245f, 245f, 245f, 0f);
                HpButton.transform.GetComponent<UnityEngine.UI.Outline>().effectColor = new Color(245f, 245f, 245f, 255f);//ѡ���
                break;
        }
    }
    //��ͣui
    public void PauseStart()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void PauseEnd()
    {
        PlayerController.isPause = false;
        PauseUI.SetActive(false);
        Time.timeScale = 1;  
    }
    //����ui
    public void _DeadUI()
    {
        DeadUI.SetActive(true);
    }
    //�����ı���غ���
    public void GetTextFromFile(TextAsset _file)
    {
        textList.Clear();//����б�
        index = 0;

        var lineDate = _file.text.Split('\n');//�Իس���Ϊһ��

        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
        StartCoroutine(SetTextUI());
    }

    public IEnumerator SetTextUI()
    {
        talkUI.SetActive(true);
        PlayerController.canWalk = false;
        textFinished = false;
        textLabel.text = "";
        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length - 1)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = textList[index];
        cancelTyping = false;
        textFinished = true;
        index++;
    }
}
