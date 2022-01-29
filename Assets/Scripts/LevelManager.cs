using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField]
    private List<GameObject> _waves;

    private int _currentWave = 0;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        StartWave(_currentWave);
    }

    private void StartWave(int id)
    {
        _waves.ForEach(x => x.SetActive(false));

        _waves[id].SetActive(true);
    }

    public void WaveComplete()
    {
        _currentWave++;
        StartWave(_currentWave);
    }
}
