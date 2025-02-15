using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunMovementState : PlayerMovementState
{
    public PlayerRunMovementState(PlayerMovementStateMachine _playerMovementStateMachine) : base(_playerMovementStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.player.currentMoveSpeed = stateMachine.player.runMoveSpeed;

        stateMachine.player.animController.AnimSetBool(stateMachine.player.animController.RunHash, true);
    }

    public override void Exit()
    {
        base.Exit();

        stateMachine.player.animController.AnimSetBool(stateMachine.player.animController.RunHash, false);
    }

    public override void HandleInput()
    {
        base.HandleInput();

        CheckToChangeNormalState();
    }

    private void CheckToChangeNormalState()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            stateMachine.ChangeState(stateMachine.normalState);
        }
    }
}
