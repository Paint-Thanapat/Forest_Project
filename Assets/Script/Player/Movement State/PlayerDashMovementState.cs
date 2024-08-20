using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashMovementState : MonoBehaviour, IPlayerMovementState
{
    private PlayerMovementController _playerMovementController;

    Vector3 dashDirection;
    float countDashDuration;
    public void Handle(PlayerMovementController movementController)
    {
        if (!_playerMovementController) // if playermovementcontroller == null
        {
            _playerMovementController = movementController;
        }

        dashDirection = _playerMovementController.movementVector;

        countDashDuration = _playerMovementController.dashDuration;

        _playerMovementController.currentMoveSpeed = 0;

        _playerMovementController.dashing = true;
    }

    void Update()
    {
        if (countDashDuration > 0)
        {
            if (_playerMovementController)
            {
                float targetAngle = Mathf.Atan2(dashDirection.x, dashDirection.z) * Mathf.Rad2Deg + _playerMovementController.cameraTransform.eulerAngles.y;

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                RaycastHit hit;
                if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), moveDir.normalized, out hit, 1.2f, _playerMovementController.dashLayerMask))
                {
                    if (hit.collider)
                    {
                        countDashDuration = 0;
                    }
                }

                if (countDashDuration <= 0)
                {
                    Debug.Log("End Dash State");
                    _playerMovementController.TransitionToNormalState();

                    countDashDuration = 0;
                    _playerMovementController.dashing = false;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (countDashDuration > 0)
        {
            if (_playerMovementController)
            {
                float targetAngle = Mathf.Atan2(dashDirection.x, dashDirection.z) * Mathf.Rad2Deg + _playerMovementController.cameraTransform.eulerAngles.y;

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                _playerMovementController.rb.MovePosition((Vector3)_playerMovementController.transform.position + (moveDir * _playerMovementController.dashMoveSpeed * Time.deltaTime));

                countDashDuration -= Time.deltaTime;

                if (countDashDuration <= 0)
                {
                    Debug.Log("End Dash State");
                    _playerMovementController.TransitionToNormalState();

                    countDashDuration = 0;
                    _playerMovementController.dashing = false;
                }
                Debug.Log("Dashing State");
            }
        }
    }
}

