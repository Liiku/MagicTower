using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool blueDoor;
    public bool violetDoor;
    public bool RedDoor;

    public AudioClip collectedClip;

    //private bool destroyDoor = false;

    private Animator Animator;
    void Awake()
    {
        Animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        AnimatorStateInfo info = Animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1 && info.IsName("OpenDoor"))
        {
            Destroy(gameObject);//���Ž�����ݻ��Լ�
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)//�ж��Ƿ�Ϊ���
        {
            if (blueDoor)
            {
                if (playerController.blueKey >= 1)
                {
                    playerController.KeyManage(-1,"BLUE");
                    Animator.SetBool("open",true);
                    playerController.PlaySound(collectedClip);
                }
            }else if (violetDoor)
            {
                if (playerController.violetKey >= 1)
                {
                    playerController.KeyManage(-1,"VIOLET");
                    Animator.SetBool("open", true);
                    playerController.PlaySound(collectedClip);
                }
            }
            else
            {
                if (playerController.redKey >= 1)
                {
                    playerController.KeyManage(-1,"RED");
                    Animator.SetBool("open", true);
                    playerController.PlaySound(collectedClip);
                }
            }
        }
    }
}
