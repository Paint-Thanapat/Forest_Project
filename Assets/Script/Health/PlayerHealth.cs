using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health, IPlayerElement
{
    private PlayerMovementController _playerMovementController;
    private PlayerInteractController _playerInteractController;

    void Start()
    {
        _playerMovementController = GetComponent<PlayerMovementController>();
        _playerInteractController = GetComponent<PlayerInteractController>();

        Invoke(nameof(SetPlayerToGameManager), 0.1f);
    }

    void SetPlayerToGameManager()
    {
        // GameManager.Instance.UIGameplay.SetPlayerHealth(this);
        GameManager.Instance.playerCharacter = this.gameObject;
    }

    public override void TakeDamage(float damage)
    {
        if (_playerMovementController.dashing)
            return;

        base.TakeDamage(damage);

        GameManager.Instance.mainCamera.StartCameraShake();

        // GameManager.Instance.UIGameplay.SetPlayerHealth(this);
    }
    public override void Die()
    {
        base.Die();
        _playerMovementController.TransitionToStopState();
        _playerInteractController.TransitionToStopState();

        GameManager.Instance.ExplosionForce(transform.position, 5, 1.5f);
        GameManager.Instance.DamageArea(transform.position, 99999f, 1000f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (currentHealth <= 500)
            {
                currentHealth = 99999f;
                // GameManager.Instance.UIGameplay.SetPlayerHealth(this);
            }
            else
            {
                currentHealth = maxHealth;
                // GameManager.Instance.UIGameplay.SetPlayerHealth(this);
            }
        }
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Deline(IVisitor visitor)
    {
        visitor.UnVisit(this);
    }
}
