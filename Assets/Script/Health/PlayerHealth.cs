using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health, IPlayerElement
{
    private Player _player;
    private PlayerInteractController _playerInteractController;

    void Start()
    {
        _player = GetComponent<Player>();
        _playerInteractController = GetComponent<PlayerInteractController>();

        Invoke(nameof(SetPlayerToGameManager), 0.1f);
    }

    void SetPlayerToGameManager()
    {
        GameManager.Instance.playerCharacter = this.gameObject;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    public override void Die()
    {
        base.Die();
        _player.ChangeState(_player.movementStateMachine.stopState);
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
