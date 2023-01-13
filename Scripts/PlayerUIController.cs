using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    //将需要获得的项目全部放在这里，功能脚本仅实现触发后调用！
    //功能也在这里实现!
    [Header("playerUI")]
    public TextMeshProUGUI HP;
    public TextMeshProUGUI ATK;
    public TextMeshProUGUI DEF;
    public TextMeshProUGUI GOLD;
    public TextMeshProUGUI EXP;
    public TextMeshProUGUI LVL;
    public TextMeshProUGUI FT;
    public TextMeshProUGUI Bluekey;
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

    //storeUI 商店UI和文本
    [Header("StoreUI")]
    public GameObject StoreUI;//商店UI
    public GameObject AttButton;
    public GameObject DefButton;
    public GameObject HpButton;
    //剧情文本
    [Header("ui")]
    public TextMeshProUGUI textLabel;
    public Image faceImage;

    public GameObject talkUI;
    public bool fristTime;

    [Header("text")]
    //public TextAsset textFile;//需要显示的文本
    public int index;
    public float textSpeed = 0.1f;//文本速度
    public bool textFinished;//是否完成打字
    bool cancelTyping;//是否取消打字

    public List<string> textList = new List<string>();//新建一个文字列表


    PlayerController PlayerController;
    PlayerInput PlayerInput;

    public GameObject FightEndUI;//战斗提示窗
    private float textTimer = 2.0f;
    private bool isTimer = false;
    //暂停ui
    public GameObject PauseUI;
    //死亡ui
    public GameObject DeadUI;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
        PlayerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        //玩家
        HP.text = "HP: " + PlayerController.playerHealth;//状态栏血量
        ATK.text = "ATK: " + PlayerController.playerAttack;//状态栏攻击
        DEF.text = "DEF: " + PlayerController.playerDefense;//状态栏防御
        GOLD.text = "GOLD: " + PlayerController.playerGold;//状态栏金币
        EXP.text = "EXP: " + PlayerController.playerExp;//状态栏经验
        LVL.text = "LVL: " + PlayerController.playerLvl;//状态栏等级
        Bluekey.text = "X " + PlayerController.blueKey;//状态栏蓝色钥匙
        Violetkey.text = "X " + PlayerController.violetKey;//状态栏紫色钥匙
        Redkey.text = "X " + PlayerController.redKey;//状态栏红色钥匙
        //敌人
        if (PlayerController.isFighting)
        {
            FightingUI.SetActive(true);
            P_HP.text = "HP: " + PlayerController.playerHealth;//玩家状态栏血量
            P_ATK.text = "ATK: " + PlayerController.playerAttack;//玩家状态栏攻击
            P_DEF.text = "DEF: " + PlayerController.playerDefense;//玩家状态栏防御

            E_HP.text = "HP: " + PlayerController.enemyHp;//敌人状态栏血量
            E_ATK.text = "ATK: " + PlayerController.enemyAtk;//敌人状态栏攻击
            E_DEF.text = "DEF: " + PlayerController.enemyDef;//敌人状态栏防御
            E_Image.sprite = PlayerController.enemyImage;
        }


       //文本结束倒计时
       if (isTimer)   
       {
          if(textTimer > 0)
          {
                textTimer -= Time.deltaTime;
          }
          else
          {
                FightEndUI.SetActive(false);
                textTimer = 2;
                isTimer = false;
          }
       }
       //如果正在购物
       if (PlayerInput.isShopping)
       {
            StoreManageText(PlayerInput.currentOption);
       }
       //如果暂停
       if (PlayerInput.isPause)
        {

            PauseStart();
        }

        //剧情文本相关判断
        if (Input.GetKeyDown(KeyCode.F) && index == textList.Count)//播放结束后关闭ui
        {
            PlayerInput.canWalk = true;
            talkUI.SetActive(false);
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

    //战斗结束文本显示
    public void FightEndText(int _Exp,int _Gold)
    {
        FightEndUI.SetActive(true);
        FT.text = "GET " + _Gold + " GOLD! GET " + _Exp + " EXP!";
        PlayerInput.canWalk = true;
        isTimer = true;
    }

    //增加生命值文本显示
    public void IncreaseHealtText(int _Hp)
    {
        FightEndUI.SetActive(true);
        FT.text = "Increase " + _Hp + " HP!";
        isTimer = true;
    }

    //增加攻击力文本显示
    public void IncreaseAttackText(int _atk)
    {
        FightEndUI.SetActive(true);
        FT.text = "Increase " + _atk + " ATK!";
        isTimer = true;
    }
    
    //增加防御力文本显示
    public void IncreaseDefenseText(int _def)
    {
        FightEndUI.SetActive(true);
        FT.text = "Increase " + _def + " DEF!";
        isTimer = true;
    }

    //钥匙文本显示
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

    //商店文本显示
    public void StoreManageText(int _numb)
    {
        StoreUI.SetActive(true);
        switch (_numb)//根据值确定当前的选项
        {
            case 0:
                AttButton.transform.GetComponent<UnityEngine.UI.Outline>().effectColor = new Color(245f,245f,245f,255f);//选择框
                DefButton.transform.GetComponent<UnityEngine.UI.Outline>().effectColor = new Color(245f,245f,245f,0f);
                HpButton.transform.GetComponent<UnityEngine.UI.Outline>().effectColor = new Color(245f,245f,245f,0f);
                break;
            case 1:
                AttButton.transform.GetComponent<UnityEngine.UI.Outline>().effectColor = new Color(245f, 245f, 245f, 0f);
                DefButton.transform.GetComponent<UnityEngine.UI.Outline>().effectColor = new Color(245f, 245f, 245f, 255f);//选择框
                HpButton.transform.GetComponent<UnityEngine.UI.Outline>().effectColor = new Color(245f, 245f, 245f, 0f);
                break;
            case 2:
                AttButton.transform.GetComponent<UnityEngine.UI.Outline>().effectColor = new Color(245f, 245f, 245f, 0f);
                DefButton.transform.GetComponent<UnityEngine.UI.Outline>().effectColor = new Color(245f, 245f, 245f, 0f);
                HpButton.transform.GetComponent<UnityEngine.UI.Outline>().effectColor = new Color(245f, 245f, 245f, 255f);//选择框
                break;
        }

        

    }
    //暂停
    public void PauseStart()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void PauseEnd()
    {
        PlayerInput.isPause = false;
        PauseUI.SetActive(false);
        Time.timeScale = 1;  
    }

    public void _DeadUI()
    {
        DeadUI.SetActive(true);
    }



    //剧情文本相关函数
    public void GetTextFromFile(TextAsset _file)
    {
        textList.Clear();//清空列表
        index = 0;

        var lineDate = _file.text.Split('\n');//以回车定为一行

        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
        StartCoroutine(SetTextUI());
    }

    public IEnumerator SetTextUI()
    {
        talkUI.SetActive(true);
        PlayerInput.canWalk = false;
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
