using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider _healthbar;
    [SerializeField] private Image _healthbarFill;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Text _waveText;

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    void OnEnable()
    {
        //Player.Instance.OnPlayerHealthChange += UpdateHealthBar;
        GameManager.Instance.OnGameOver += GameOverUI;
    }


    public IEnumerator DisplayWaveText(int wave)
    {
        if (wave == 2)
        {
            _waveText.text = "Final Wave";
        }
        else
        {
            _waveText.text = $"Wave {wave + 1}";
        }
        _waveText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        _waveText.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateHealthBar(Player.Instance._health);
    }

    private void UpdateHealthBar(float amount)
    {
        _healthbar.value = amount;

        if (amount > 65)
        {
            _healthbarFill.color = new Color(0, 1, 0.59071640f, 1);
        }
        else if (amount > 25)
        {
            _healthbarFill.color = new Color(.9804546f, 1, .5896226f, 1);
        }
        else
        {
            _healthbarFill.color = new Color(1, 0.2559966f, 0.1462264f, 1);
        }
    }

    private void GameOverUI()
    {
        gameOverUI.SetActive(true);
    }

    public void LoadScene(int id) //Better way to do this so that Menu can access this function too?
    {
        gameOverUI.SetActive(false);
        SceneManager.LoadScene(id);
    }

/*    void OnDisable()
    {
        Player.Instance.OnPlayerHealthChange -= UpdateHealthBar;
        GameManager.Instance.OnGameOver -= GameOverUI;
    }*/
}
