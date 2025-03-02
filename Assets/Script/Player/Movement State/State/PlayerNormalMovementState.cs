using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalMovementState : PlayerMovementState
{
    public PlayerNormalMovementState(PlayerMovementStateMachine _playerMovementStateMachine) : base(_playerMovementStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.player.currentMoveSpeed = stateMachine.player.normalMoveSpeed;

        stateMachine.player.canDash = true;
    }

    public override void HandleInput()
    {
        stateMachine.player.animController.AnimSetBool(stateMachine.player.animController.WalkHash, stateMachine.player.movementVector != Vector3.zero);

        base.HandleInput();

        CheckToChangeRunState();
    }

    public override void Exit()
    {
        base.Exit();

        stateMachine.player.animController.AnimSetBool(stateMachine.player.animController.WalkHash, false);
    }

    private void CheckToChangeRunState()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            stateMachine.ChangeState(stateMachine.runState);
        }
    }
}
