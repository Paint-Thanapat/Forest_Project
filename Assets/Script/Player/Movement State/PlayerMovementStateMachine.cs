using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public Player player { get; }
    public PlayerNormalMovementState normalState { get; }
    public PlayerSlowMovementState slowState { get; }
    public PlayerDashMovementState dashState { get; }
    public PlayerStopMovementState stopState { get; }

    public PlayerMovementStateMachine(Player _player)
    {
        player = _player;

        normalState = new PlayerNormalMovementState(this);

        slowState = new PlayerSlowMovementState(this);

        dashState = new PlayerDashMovementState(this);

        stopState = new PlayerStopMovementState(this);
    }
}