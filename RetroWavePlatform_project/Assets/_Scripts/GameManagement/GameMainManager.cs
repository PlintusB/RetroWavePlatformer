using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainManager : MonoBehaviour
{
    [SerializeField] private UIManager _UIManager;

    [SerializeField] private GameObject _loseGameWindow;
    [SerializeField] private GameObject _winLevelWindow;

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
            if (_currentHealth == 0) EventManager.OnPlayerDied.Invoke();
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
        CurrentHealth = _maxPlayerHealth;

        EventManager.OnLevelScoreChanged
                    .AddListener(SetLevelScoreValue);
        EventManager.OnPlayerHealthChanged
                    .AddListener(IncreaseCurrentHealthValue);
        EventManager.OnDamageReceived
                    .AddListener(DecreaseCurrentHealthValue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            CurrentHealth -= 20;
            CurrentScore += 7;
        }
    }

    void IncreaseCurrentHealthValue(int health) =>
        CurrentHealth += health;

    void SetLevelScoreValue(int value) =>
        CurrentScore += value;

    void DecreaseCurrentHealthValue(int damage, Vector2 dir) =>
        CurrentHealth -= damage;
}
