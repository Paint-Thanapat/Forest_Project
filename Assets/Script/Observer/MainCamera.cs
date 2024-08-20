using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraView { LockPlayerPosition, FollowMouse }

public class MainCamera : Observer
{
    public CameraView cameraView;
    private PlayerInteractController _playerInteractController;

    private Vector3 _offset;
    public Vector3 focusPoint;

    private Vector3 _playerPoint;
    private Vector3 _aimPoint;

    public Vector3 currentCameraPosition;

    [Header("Camera Shake")]
    public bool isShaking;
    public float shakeDuration = 0.2f;
    public float shakeMagnitude;
    void Start()
    {
        _offset = transform.position;

        GameManager.Instance.mainCamera = this;
    }

    public void SwitchCameraMode()
    {
        if (cameraView == CameraView.LockPlayerPosition)
        {
            cameraView = CameraView.FollowMouse;
        }
        else if (cameraView == CameraView.FollowMouse)
        {
            cameraView = CameraView.LockPlayerPosition;
        }
    }

    void FixedUpdate()
    {
        if (_playerInteractController != null)
        {

            focusPoint = ((_playerPoint + (_aimPoint)));
            //Debug.Log(focusPoint);
        }

        currentCameraPosition = Vector3.Slerp(transform.position, (focusPoint / 2) + _offset, 10f * Time.deltaTime);

        if (isShaking)
        {
            gameObject.transform.position = currentCameraPosition + (Random.insideUnitSphere * shakeMagnitude);
        }
        else
        {
            transform.position = currentCameraPosition;
        }

        //Debug.Log(_offset);
    }

    public void StartCameraShake()
    {
        StartCoroutine(CameraShake());
    }

    public IEnumerator CameraShake()
    {
        float shakingTime = shakeDuration;
        while (shakingTime > 0)
        {
            isShaking = true;
            shakingTime -= Time.deltaTime;
            yield return null;
        }

        isShaking = false;
    }

    public override void Notify(Subject subject)
    {
        if (!_playerInteractController)
        {
            _playerInteractController = subject.GetComponent<PlayerInteractController>();
        }

        if (_playerInteractController)
        {
            if (_playerPoint != _playerInteractController.transform.position)
            {
                _playerPoint = _playerInteractController.transform.position;
            }

            if (cameraView == CameraView.LockPlayerPosition)
            {
                if (_aimPoint != _playerInteractController.transform.position)
                {
                    _aimPoint = _playerInteractController.transform.position;
                }
            }
            if (cameraView == CameraView.FollowMouse)
            {
                if (_aimPoint != _playerInteractController.aimPoint)
                {
                    _aimPoint = _playerInteractController.aimPoint;
                }
            }
        }
    }
}
