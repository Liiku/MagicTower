using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextDoor : MonoBehaviour
{
    //����״ν��봥������ɴ��ͣ�����ʱfristtrigger��Ϊfalse��������������ٴδ���fristtrigger��Ϊtrue����ʱ�뿪�������ٴν������ٴδ��ͣ�
    public Transform TargetTransform;//Ŀ�����
    private bool fristOnTrigger = true;//����״ν���
    void OnTriggerEnter2D(Collider2D other)
    {
        if (fristOnTrigger)//������״ν���
        {
            other.gameObject.transform.position = TargetTransform.transform.position;//���͵�Ŀ���
            fristOnTrigger = false;//�״ν���Ϊ��
        }
        else
        {
            fristOnTrigger = true;//�ٴδ������״ν�����Ϊtrue
        }
    }
}
        
