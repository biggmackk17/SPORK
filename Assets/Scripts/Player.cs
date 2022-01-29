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

    private int _health = 100;
    private int _totalHealth = 100;

    
    private void Awake()
    {
        _instance = this;
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;
        OnPlayerHealthChange?.Invoke((float)_health/_totalHealth);
        if(_health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        _health += amount;
        OnPlayerHealthChange?.Invoke((float)_health/_totalHealth);
    }

    private void Die()
    {
        //YOU DEAD
    }
}
