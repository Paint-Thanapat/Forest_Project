using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopMovementState : PlayerMovementState
{
    public PlayerStopMovementState(PlayerMovementStateMachine _playerMovementStateMachine) : base(_playerMovementStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.player.currentMoveSpeed = 0;
    }
}
