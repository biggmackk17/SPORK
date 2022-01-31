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

    static private int _currentWave = 0;
    [SerializeField]private int _enemiesLeft;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        StartWave(_currentWave);
        Enemy.OnEnemyDie += DecrementEnemiesLeft;
    }

    private void StartWave(int id)
    {
        StartCoroutine(UIManager.Instance.DisplayWaveText(id));
        _waves.ForEach(x => x.gameObject.SetActive(false));
        _waves[id].gameObject.SetActive(true);
        _enemiesLeft = _waves[id].EnemyCount;
        Player.Instance.transform.position = Vector3.zero;
    }

    public void WaveComplete()
    {
        if (_currentWave == 3)
        {
            GameManager.isGameOver = true;
            Debug.Log("END OF GAME");
            UIManager.Instance.GameComplete();
        }
        if (!GameManager.isGameOver)
        {
            _currentWave++;
            StartCoroutine(WaveDelay());
            //StartWave(_currentWave);
        }
    }

    private IEnumerator WaveDelay()
    {
        AudioManager.Instance.CombatMusic(false);
        AudioManager.Instance.PlayAudioClip(AudioManager.Instance._reactionClips[1]);
        //Downtime interaction?
        yield return new WaitForSeconds(6f);
        AudioManager.Instance.CombatMusic(true);
        StartWave(_currentWave);
    }

   public void DecrementEnemiesLeft()
    {
        _enemiesLeft--;
        if (_enemiesLeft <= 0)
        {
            WaveComplete();
        }
    }


}
