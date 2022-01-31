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
    private List<Wave> _waves;

    static public int _currentWave = 0;
    public int waveViewer;
    [SerializeField] private int _enemiesLeft;

    private void Awake()
    {
        _instance = this;
        //_currentWave = 0;
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDie += DecrementEnemiesLeft;
    }

    private void Start()
    {
        StartWave(_currentWave);
    }

    private void StartWave(int id)
    {
        StartCoroutine(UIManager.Instance.DisplayWaveText(id));
        _waves.ForEach(x => x.gameObject.SetActive(false));
        _waves[id].gameObject.SetActive(true);
        _enemiesLeft = _waves[id].EnemyCount;
        Player.Instance.transform.position = Vector3.zero;
    }

    private void Update()
    {
        waveViewer = _currentWave;
    }

    public void WaveComplete()
    {
        if (_currentWave == 3)
        {
            GameManager.Instance.isGameOver = true;
            Debug.Log("END OF GAME");
            UIManager.Instance.GameComplete();
        }
        if (!GameManager.Instance.isGameOver)
        {
            _currentWave++;
            StartCoroutine(DelayedWaveStart());
        }
    }

    private IEnumerator DelayedWaveStart()
    {
        AudioManager.Instance.CombatMusic(false);
        AudioManager.Instance.PlayAudioClip(AudioManager.Instance._reactionClips[1]);
        //Downtime interaction?
        yield return new WaitForSeconds(6f);
        if (!GameManager.Instance.isGameOver)
        {
            AudioManager.Instance.CombatMusic(true);
            StartWave(_currentWave);
        }
    }

   public void DecrementEnemiesLeft()
    {
        if (!GameManager.Instance.isGameOver)
        {
            _enemiesLeft--;
            if (_enemiesLeft <= 0)
            {
                WaveComplete();
            }
        }
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDie -= DecrementEnemiesLeft;
    }
}
