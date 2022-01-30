using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class Player : MonoBehaviour, IDamageable
{

    private static Player _instance;
    public static Player Instance => _instance;

    public Action<float> OnPlayerHealthChange;

    private float _health = 100;
    private float _totalHealth = 100;
    private bool _invincible;

    private void Awake()
    {
        _instance = this;
    }

    public void TakeDamage(float amount)
    {
        if (!_invincible)
        {
            _health -= amount;
            OnPlayerHealthChange?.Invoke(_health);
            if (_health <= 0)
            {
                Die();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PickUps>(out var pickup))
        {
            if (pickup.Type == PickUps._powerUpType.Health)
            {
                Heal(pickup.HealAmount);
            }
            else if (pickup.Type == PickUps._powerUpType.SporkTime)
            {
                SetInvincible();
            }
            Destroy(pickup.gameObject);
        }
    }
    private IEnumerator SetInvincible()
    {
        _invincible = true;
        //play invinicble animation
        yield return new WaitForSeconds(5f);
        _invincible = false;
    }

    public void Heal(float amount)
    {
        _health += amount;
        OnPlayerHealthChange?.Invoke(_health);
    }

    private void Die()
    {
        //YOU DEAD
    }
}
