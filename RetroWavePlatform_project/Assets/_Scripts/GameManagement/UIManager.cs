using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class UIManager : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private GameMainManager _gameManager;

    [Header("Score settings")]
    [SerializeField] private TextMeshProUGUI _scorePanelText;
    private float _currentScore;

    [Header("Timer settings")]
    [SerializeField] private TextMeshProUGUI _levelTimerText;
    private float _currentTime;
    private int _currentSeconds;
    private int _currentMinutes;

    [Header("HealthBar settings")]
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private Gradient _healthBarGradient;
    [SerializeField] private Image _healthBarFill;
    [SerializeField, Range(0, 2)] private float _healthBarSmoothSpeed;
    private float _maxPlayerHealth;
    private float _currentHealth;
    private Coroutine HealthBarSmoothCoroutine;
    private float _healthBarSmoothVelocity;

    private void Awake()
    {


    }

    void Start()
    {
        _maxPlayerHealth = _gameManager.MaxPlayerHealth;
        _currentHealth = _gameManager.CurrentHealth;
        _healthBarSlider.maxValue = _maxPlayerHealth;
        RefreshHealthBarPanel();

        _currentScore = _gameManager.CurrentScore;
        RefreshScorePanel();
    }

    void OnEnable()
    {
        _currentTime = 0;
    }

    void Update()
    {
        RefreshTimerPanel();
    }

    public async void RefreshScorePanel()
    {
        float targetScore = _gameManager.CurrentScore;
        if (_currentScore == targetScore) return;

        while(true)
        {
            _currentScore++;
            if (_currentScore < 10)
                _scorePanelText.text = "00" + _currentScore.ToString();
            else if (_currentScore < 100)
                _scorePanelText.text = "0" + _currentScore.ToString();
            else _scorePanelText.text = _currentScore.ToString();

            if(_currentScore >= targetScore)
            {
                _currentScore = targetScore;
                return;
            }

            await Task.Yield();
        }
    }

    public void RefreshHealthBarPanel()
    {
        _currentHealth = _gameManager.CurrentHealth;
        HealthBarSmoothCoroutine = StartCoroutine(HealthBarSmoothing());
    }

    IEnumerator HealthBarSmoothing ()
    {
        while(true)
        {
            _healthBarSlider.value =
            Mathf.SmoothDamp(_healthBarSlider.value,
                             _currentHealth,
                             ref _healthBarSmoothVelocity,
                             _healthBarSmoothSpeed);
            _healthBarFill.color = 
                _healthBarGradient.Evaluate(_healthBarSlider.normalizedValue);
            yield return null;
            if (_healthBarSlider.value == _currentHealth)
                StopCoroutine(HealthBarSmoothCoroutine);
        }        
    }

    void RefreshTimerPanel()
    {
        _currentTime += Time.deltaTime;
        int rawCurrentTime = (int)Mathf.Floor(_currentTime);
        _currentSeconds = rawCurrentTime % 60;
        _currentMinutes = (rawCurrentTime - _currentSeconds) / 60;

        _levelTimerText.text =
            FormatsTimeValue(_currentMinutes)
            + ":"
            + FormatsTimeValue(_currentSeconds);
    }

    string FormatsTimeValue (int value)
    {
        return value < 10
            ? "0" + value.ToString()
            : value.ToString();
    }
}
