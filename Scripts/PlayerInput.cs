using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float playerSpeed;//�ƶ��ٶ�
    public bool isWalk;
    public bool canWalk = true;
    public bool isShopping = false;
    public bool isPause = false;


    public int currentOption = 0;//��ǰѡ��//ѡ���ֵΪ0��1��2������ѡ��
    int optionNumber = 2;//�ܹ�3��ѡ��

    SpriteRenderer SpriteRenderer;
    PlayerController PlayerController;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
        SpriteRenderer = GetComponent<SpriteRenderer>();//ͼ�����ҷ�ת
    }

    void Update()
    {
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



        //��ͣ
        if (Input.GetKeyDown(KeyCode.E))
        {
            isPause = true;
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

}
