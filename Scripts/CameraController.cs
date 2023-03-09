using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 currentPos;//��ǰλ��
    private Vector3 targetPos;//Ŀ��λ��
    [SerializeField] private Transform _camera;//�����λ��
    [SerializeField] private float _smoothTime = 1.0f; // Ŀ�˂��˵��_����ޤǤΤ����褽�Εr�g[s]
    [SerializeField] private float _maxSpeed = float.PositiveInfinity;// ����ٶ�
    private float _currentVelocity = 0;// �F���ٶ�(SmoothDamp��Ӌ��Τ���˱�Ҫ)

    private void Awake()
    {
        currentPos = _camera.position;
        targetPos = _camera.position;
    }
    // x���ˤ򥿩`���åȤ�x���ˤ�׷��������
    private void Update()
    {
        currentPos.y = Mathf.SmoothDamp(currentPos.y,targetPos.y,ref _currentVelocity,_smoothTime,_maxSpeed);
        _camera.position = currentPos;
    }
    public void Camera(bool _upordown)
    {
      if (_upordown)//�����¥����ǰλ��+14=Ŀ��λ��
      {
            targetPos.y = targetPos.y + 14.0f;
      }
      else
        {
            targetPos.y = targetPos.y - 14.0f;
        }
    }
}
