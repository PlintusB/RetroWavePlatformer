using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainManager : MonoBehaviour
{
    [SerializeField] private UIManager _UIManager;

    [SerializeField] private GameObject _loseGameWindow;
    [SerializeField] private GameObject _winLevelWindow;
    [SerializeField] private GameObject _gameCanvases;

    [SerializeField] private float _maxPlayerHealth;
    public float MaxPlayerHealth
    {
        get { return _maxPlayerHealth; }
        private set { _maxPlayerHealth = value; }
    }

    private float _currentHealth;
    public float CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            if (value < 0) value = 0;
            if (value > MaxPlayerHealth) value = MaxPlayerHealth;
            _currentHealth = value;
            _UIManager.RefreshHealthBarPanel();
            if (_currentHealth <= 0) EventManager.OnPlayerDied.Invoke();
        }
    }

    private float _currentScore;
    public float CurrentScore
    {
        get { return _currentScore; }
        set
        {
            _currentScore = value;
            _UIManager.RefreshScorePanel();
        }
    }

    private void Awake()
    {
        Time.timeScale = 1f;

        CurrentHealth = _maxPlayerHealth;
        _loseGameWindow.SetActive(false);
        _winLevelWindow.SetActive(false);
        _gameCanvases.SetActive(true);

        EventManager.OnLevelScoreChanged
                    .AddListener(SetLevelScoreValue);
        EventManager.OnPlayerHealthChanged
                    .AddListener(IncreaseCurrentHealthValue);
        EventManager.OnDamageReceived
                    .AddListener(DecreaseCurrentHealthValue);
        EventManager.OnLevelLoseEnded
                    .AddListener(OpenLoseGameWindow);
        EventManager.OnLevelWinEnded
                    .AddListener(OpenWinLevelWindow);
    }

    private void IncreaseCurrentHealthValue(int health) =>
        CurrentHealth += health;

    private void SetLevelScoreValue(int value) =>
        CurrentScore += value;

    private void DecreaseCurrentHealthValue(int damage, Vector2 dir) =>
        CurrentHealth -= damage;

    private void OpenLoseGameWindow()
    {
        FinishedLevel();
        _loseGameWindow.SetActive(true);
    }

    private void OpenWinLevelWindow()
    {
        FinishedLevel();
        _winLevelWindow.SetActive(true);
    }

    private void FinishedLevel()
    {
        Time.timeScale = 0f;
        _gameCanvases.SetActive(false);
    }
}
