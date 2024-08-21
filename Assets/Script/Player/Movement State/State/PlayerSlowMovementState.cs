using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlowMovementState : PlayerMovementState
{
    public PlayerSlowMovementState(PlayerMovementStateMachine _playerMovementStateMachine) : base(_playerMovementStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.player.currentMoveSpeed = stateMachine.player.slowMoveSpeed;
    }
}
