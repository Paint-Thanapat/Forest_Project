using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathMovementState : PlayerMovementState
{
    float respawnDelay = 5f;

    float respawnCounter;

    public PlayerDeathMovementState(PlayerMovementStateMachine _playerMovementStateMachine) : base(_playerMovementStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        respawnCounter = respawnDelay;
    }

    public override void Update()
    {
        base.Update();

        if (respawnCounter > 0)
        {
            respawnCounter -= Time.deltaTime;

            if (respawnCounter <= 0)
            {
                stateMachine.ChangeState(stateMachine.normalState);
            }
        }
    }

    public override void PhysicsUpdate()
    {

    }

    public override void HandleInput()
    {

    }
}
