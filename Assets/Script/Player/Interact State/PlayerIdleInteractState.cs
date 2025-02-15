using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleInteractState : MonoBehaviour, IPlayerInteractState
{
    private PlayerInteractController _playerInteractController;

    public void Handle(PlayerInteractController interactController)
    {
        if (!_playerInteractController) // if _playerInteractController == null
        {
            _playerInteractController = interactController;
        }
    }
}