using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextDoor : MonoBehaviour
{
    //����״ν��봥������ɴ��ͣ�����ʱfristtrigger��Ϊfalse��������������ٴδ���fristtrigger��Ϊtrue����ʱ�뿪�������ٴν������ٴδ��ͣ�
    //private bool fristOnTrigger = true;//����״ν���
    public GameObject TargetTransform;
    //public string targetFloor;
    public bool upFloor;
    public bool DownFloor;

    void OnTriggerEnter2D(Collider2D other)
    { 
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController.floor_fristOnTrigger)//������״ν���
        {
            other.gameObject.transform.position = TargetTransform.transform.position;//���͵�Ŀ���
            playerController.floor_startCountDown = true;
            if (upFloor)
            {
                playerController.UpFloor();
            }
            else
            {
                playerController.DownFloor();
            }
            //SceneManager.LoadScene(targetFloor);
            playerController.floor_fristOnTrigger = false;//�״ν���Ϊ��
        }
        else
        {
            playerController.floor_fristOnTrigger = true;//�ٴδ������״ν�����Ϊtrue
        }
    }
}
        
