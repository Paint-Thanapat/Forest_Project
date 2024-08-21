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
}
