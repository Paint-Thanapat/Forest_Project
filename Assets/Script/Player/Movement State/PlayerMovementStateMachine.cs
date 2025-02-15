using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public Player player { get; }
    public PlayerNormalMovementState normalState { get; }
    public PlayerRunMovementState runState { get; }
    public PlayerSlowMovementState slowState { get; }
    public PlayerDashMovementState dashState { get; }
    public PlayerStopMovementState stopState { get; }
    public PlayerJumpMovementState jumpState { get; }
    public PlayerSleepMovementState sleepState { get; }

    public PlayerMovementStateMachine(Player _player)
    {
        player = _player;

        normalState = new PlayerNormalMovementState(this);

        runState = new PlayerRunMovementState(this);

        slowState = new PlayerSlowMovementState(this);

        dashState = new PlayerDashMovementState(this);

        stopState = new PlayerStopMovementState(this);

        jumpState = new PlayerJumpMovementState(this);

        sleepState = new PlayerSleepMovementState(this);
    }
}