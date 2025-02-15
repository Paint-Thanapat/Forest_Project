using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpMovementState : PlayerMovementState
{
    Vector3 movementDir;
    float jumpForce;

    public PlayerJumpMovementState(PlayerMovementStateMachine _playerMovementStateMachine) : base(_playerMovementStateMachine)
    {
        jumpForce = stateMachine.player.jumpForce;
    }

    public override void Enter()
    {
        base.Enter();

        movementDir = GetMoveDirection();

        if (stateMachine.player.movementVector == Vector3.zero)
        {
            stateMachine.player.rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
        else
        {
            stateMachine.player.rb.AddForce((movementDir + Vector3.up) * jumpForce, ForceMode.VelocityChange);
        }

        stateMachine.player.canJump = false;
    }

    public override void PhysicsUpdate()
    {
        if (stateMachine.player.rb.velocity.y < 0)
        {
            if (Physics.Raycast(stateMachine.player.transform.position, -Vector3.up, 0.2f, stateMachine.player.jumpContractMask))
            {
                stateMachine.ChangeState(stateMachine.normalState);
            }
        }
    }

    public override void HandleInput()
    {

    }

    public override void Exit()
    {
        base.Exit();

        stateMachine.player.canJump = true;
    }
}
