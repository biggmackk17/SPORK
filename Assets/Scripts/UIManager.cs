using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider _healthbar;
    [SerializeField] private Image _healthbarFill;
    private void Start()
    {
        Player.Instance.OnPlayerHealthChange += UpdateHealthBar;
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
}
