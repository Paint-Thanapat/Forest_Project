using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateContext : MonoBehaviour
{
    public IPlayerMovementState currentMovementState { get; set; }

    private readonly PlayerMovementController _playerMovementController;

    public PlayerMovementStateContext(PlayerMovementController movementController)
    {
        _playerMovementController = movementController;
    }

    public void Transition()
    {
        currentMovementState.Handle(_playerMovementController);
    }

    public void Transition(IPlayerMovementState state)
    {
        currentMovementState = state;
        currentMovementState.Handle(_playerMovementController);
    }
}
