using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextDoor : MonoBehaviour
{
    //玩家首次进入触发器后可传送，传送时fristtrigger变为false，传送完成立即再次触发fristtrigger变为true；此时离开触发器再次进入便可再次传送；
    public Transform TargetTransform;//目标组件
    private bool fristOnTrigger = true;//玩家首次进入
    void OnTriggerEnter2D(Collider2D other)
    {
        if (fristOnTrigger)//如果是首次进入
        {
            other.gameObject.transform.position = TargetTransform.transform.position;//传送到目标层
            fristOnTrigger = false;//首次进入为否
        }
        else
        {
            fristOnTrigger = true;//再次触发则将首次进入设为true
        }
    }
}
        
