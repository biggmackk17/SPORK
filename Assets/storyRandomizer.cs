using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class storyRandomizer : MonoBehaviour
{
    [SerializeField] TMP_Text _storyText;
    [SerializeField] string[] _story; 
    private void OnEnable()
    {
        _storyText.text = _story[Random.Range(0, _story.Length)];
    }
}
