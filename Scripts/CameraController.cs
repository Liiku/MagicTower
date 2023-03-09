using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 currentPos;//当前位置
    private Vector3 targetPos;//目标位置
    [SerializeField] private Transform _camera;//摄像机位置
    [SerializeField] private float _smoothTime = 1.0f; // 目標値に到達するまでのおおよその時間[s]
    [SerializeField] private float _maxSpeed = float.PositiveInfinity;// 最高速度
    private float _currentVelocity = 0;// 現在速度(SmoothDampの計算のために必要)

    private void Awake()
    {
        currentPos = _camera.position;
        targetPos = _camera.position;
    }
    // x座標をターゲットのx座標に追従させる
    private void Update()
    {
        currentPos.y = Mathf.SmoothDamp(currentPos.y,targetPos.y,ref _currentVelocity,_smoothTime,_maxSpeed);
        _camera.position = currentPos;
    }
    public void Camera(bool _upordown)
    {
      if (_upordown)//如果上楼，当前位置+14=目标位置
      {
            targetPos.y = targetPos.y + 14.0f;
      }
      else
        {
            targetPos.y = targetPos.y - 14.0f;
        }
    }
}
