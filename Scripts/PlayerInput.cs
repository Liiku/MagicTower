using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float playerSpeed;//移动速度
    public bool isWalk;
    public bool canWalk = true;
    public bool isShopping = false;
    public bool isPause = false;


    public int currentOption = 0;//当前选项//选项的值为0，1，2的三个选项
    int optionNumber = 2;//总共3个选项

    SpriteRenderer SpriteRenderer;
    PlayerController PlayerController;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
        SpriteRenderer = GetComponent<SpriteRenderer>();//图像左右翻转
    }

    void Update()
    {
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
                    PlayerController.AttackManage(5);
                }
                else if (currentOption == 1)
                {
                    PlayerController.DefenseManage(5);
                }
                else
                {
                    PlayerController.HealthManage(400);
                }

            }
            
        }
        //
        //if (isShopping || Input.GetKeyDown(KeyCode.E))
        //{
        //    isShopping = false;
        //    canWalk = true;
        //}



        //暂停
        if (Input.GetKeyDown(KeyCode.E))
        {
            isPause = true;
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

}
