using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider _healthbar;
    [SerializeField] private Image _healthbarFill;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Text _waveText;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    private string[] _gameOverStories =
    {
        "The utensil purists of the world will now run rampant.",
        "Utensils, what we do in life… echoes in eternity.",
        "You were tossed into a riverbank and left to rust.",
        "“The Spork and his people have long been prejudiced, and the road to equality is an uphill slog, fraught with innumerable opposition, but I stand with you, brothers and sisters.” – Gandhi",
        "Sporktacus, the last hope, has fallen leaving Emperor Cauliflower to oppress all who oppose his rule.",
        "You splintered like cheap plastic takeout",
        "Too dull to puncture, to flat to scoop. A pathetic excuse for a Spork."
    };

    private static UIManager _instance;
    [SerializeField]private GameObject _WinMenu;

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
        if (wave == 3)
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
        _gameOverText.text = _gameOverStories[UnityEngine.Random.Range(0, _gameOverStories.Length)];
    }

    public void LoadScene(int id) //Better way to do this so that Menu can access this function too?
    {
        gameOverUI.SetActive(false);
        SceneManager.LoadScene(id);
    }

    public void GameComplete()
    {
        _WinMenu.SetActive(true);
    }
}
