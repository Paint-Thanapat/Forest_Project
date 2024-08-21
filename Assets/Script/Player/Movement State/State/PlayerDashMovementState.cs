using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashMovementState : PlayerMovementState
{
    Vector3 dashDirection;
    float countDashDuration;

    public PlayerDashMovementState(PlayerMovementStateMachine _playerMovementStateMachine) : base(_playerMovementStateMachine)
    {

    }

    #region IState Methods

    public override void Enter()
    {
        base.Enter();

        dashDirection = stateMachine.player.movementVector;

        countDashDuration = stateMachine.player.dashDuration;

        stateMachine.player.currentMoveSpeed = 0;

        stateMachine.player.dashing = true;
    }

    public override void Update()
    {
        if (countDashDuration > 0)
        {
            float targetAngle = Mathf.Atan2(dashDirection.x, dashDirection.z) * Mathf.Rad2Deg + stateMachine.player.cameraTransform.eulerAngles.y;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            RaycastHit hit;
            if (Physics.Raycast(stateMachine.player.transform.position + (Vector3.up * 0.1f), moveDir.normalized, out hit, 1.2f, stateMachine.player.dashLayerMask))
            {
                if (hit.collider)
                {
                    countDashDuration = 0;
                }
            }

            if (countDashDuration <= 0)
            {
                Debug.Log("End Dash State");
                stateMachine.ChangeState(stateMachine.normalState);

                countDashDuration = 0;
                stateMachine.player.dashing = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        if (countDashDuration > 0)
        {
            float targetAngle = Mathf.Atan2(dashDirection.x, dashDirection.z) * Mathf.Rad2Deg + stateMachine.player.cameraTransform.eulerAngles.y;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            stateMachine.player.rb.MovePosition((Vector3)stateMachine.player.transform.position + (moveDir * stateMachine.player.dashMoveSpeed * Time.deltaTime));

            countDashDuration -= Time.deltaTime;

            if (countDashDuration <= 0)
            {
                Debug.Log("End Dash State");
                stateMachine.ChangeState(stateMachine.normalState);

                countDashDuration = 0;
                stateMachine.player.dashing = false;
            }
            Debug.Log("Dashing State");
        }
    }

    #endregion

}

