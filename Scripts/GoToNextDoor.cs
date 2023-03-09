using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextDoor : MonoBehaviour
{
    //玩家首次进入触发器后可传送，传送时fristtrigger变为false，传送完成立即再次触发fristtrigger变为true；此时离开触发器再次进入便可再次传送；
    //private bool fristOnTrigger = true;//玩家首次进入
    public GameObject TargetTransform;
    //public string targetFloor;
    public bool upFloor;
    public bool DownFloor;

    void OnTriggerEnter2D(Collider2D other)
    { 
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController.floor_fristOnTrigger)//如果是首次进入
        {
            other.gameObject.transform.position = TargetTransform.transform.position;//传送到目标层
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
            playerController.floor_fristOnTrigger = false;//首次进入为否
        }
        else
        {
            playerController.floor_fristOnTrigger = true;//再次触发则将首次进入设为true
        }
    }
}
        
