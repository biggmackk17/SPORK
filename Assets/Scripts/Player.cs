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

    
    private void Awake()
    {
        _instance = this;
    }

    public void TakeDamage(float amount)
    {
        _health -= amount;
        OnPlayerHealthChange?.Invoke(_health);
        if(_health <= 0)
        {
            Die();
        }
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
