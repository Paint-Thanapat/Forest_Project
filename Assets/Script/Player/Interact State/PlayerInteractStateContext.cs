using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractStateContext : MonoBehaviour
{
    public IPlayerInteractState currentInteractState { get; set; }

    private readonly PlayerInteractController _playerInteractController;

    public PlayerInteractStateContext(PlayerInteractController interactController)
    {
        _playerInteractController = interactController;
    }

    public void Transition()
    {
        currentInteractState.Handle(_playerInteractController);
    }

    public void Transition(IPlayerInteractState state)
    {
        currentInteractState = state;
        currentInteractState.Handle(_playerInteractController);
    }
}
