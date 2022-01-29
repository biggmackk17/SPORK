using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{

    private static Player _instance;
    public static Player Instance => _instance;

    private int _health;

    
    private void Awake()
    {
        _instance = this;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //YOU DEAD
    }
}
