using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackInteractState : MonoBehaviour, IPlayerInteractState
{
    private PlayerInteractController _playerInteractController;

    public void Handle(PlayerInteractController interactController)
    {
        if (!_playerInteractController) // if _playerInteractController == null
        {
            _playerInteractController = interactController;
        }

        // _playerInteractController.playerWeaponHolder.Reset();
        // _playerInteractController.playerWeaponHolder.Decorate();
        // _playerInteractController.playerWeaponHolder.ToggleFire();
    }

    void Update()
    {
        if (_playerInteractController)
        {
            if (_playerInteractController.thisIsCurrentState(this))
            {
                _playerInteractController.CastAimPoint();
            }
        }
    }
}
