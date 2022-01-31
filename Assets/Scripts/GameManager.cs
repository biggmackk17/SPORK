using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public static bool isGameOver = false;

    public Action OnGameOver;

    void Awake()
    {
        _instance = this;
        Debug.Log(_instance);
    }

    public void GameOver()
    {
        isGameOver = true;
        OnGameOver?.Invoke();
    }
}
