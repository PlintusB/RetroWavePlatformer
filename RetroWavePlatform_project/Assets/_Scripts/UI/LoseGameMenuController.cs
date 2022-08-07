using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseGameMenuController : MonoBehaviour
{
    [SerializeField] private GameMainManager _gameManager;

    [SerializeField] private Text _totalLevelScore;
    [SerializeField] private Text _totalLevelTime;
    [SerializeField] private TextMeshProUGUI _gameTimerText;

    void OnEnable()
    {
        _totalLevelTime.text = _gameTimerText.text;
        _totalLevelScore.text = _gameManager.CurrentScore.ToString();
    }

}
