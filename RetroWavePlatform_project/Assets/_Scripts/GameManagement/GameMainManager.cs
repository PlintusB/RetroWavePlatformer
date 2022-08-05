using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainManager : MonoBehaviour
{
    [SerializeField] private float _maxPlayerHealth;
    public float MaxPlayerHealth
    {
        get { return _maxPlayerHealth; }
        private set { _maxPlayerHealth = value; }
    }

    private void Awake()
    {
        CurrentHealth = _maxPlayerHealth;

        EventManager.OnHeartBonusTook.AddListener(Sample);
        EventManager.OnHeartBonusTook.AddListener(SetCurrentHealthValue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            CurrentHealth -= 20;
            CurrentScore += 7;
        }

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
        }
    }

    void SetCurrentHealthValue(int value, int health)
    {
        CurrentHealth += health;
        CurrentScore += value;
    }

    public float CurrentScore { get; set; }


    [SerializeField] private GameObject _loseGameWindow;
    [SerializeField] private GameObject _winLevelWindow;

    private int currentMinutes;
    private float currentSeconds;
    public bool IsTimerTurnedOn { get; set; }



    void Sample (int value, int health)
    {
        print($"Получено {value} монет и {health} здоровья");
    }

    //private int gameScore;
    //public int GameScore
    //{
    //    get { return gameScore; }
    //    set
    //    {
    //        gameScore = value;
    //        RefreshScoreboard();
    //    }
    //}

    //void Start()
    //{
    //    ResetGameValues();
    //    CountdownToStartGame();
    //}

    //void Update()
    //{
    //    Countdown();
    //    RefreshTimeCountdown();
    //}

    //private void Countdown()
    //{
    //    if (IsTimerTurnedOn)
    //    {
    //        currentSeconds -= Time.deltaTime;

    //        if (currentSeconds < 0)
    //        {
    //            if (currentMinutes > 0)
    //            {
    //                currentMinutes--;
    //                currentSeconds = 60;
    //            }
    //            else
    //            {
    //                currentSeconds = 0;
    //                FreezeTime(true);
    //                endGameWindow.SetActive(true);
    //                IsTimerTurnedOn = false;
    //            }
    //        }
    //    }
    //}

    //private void RefreshScoreboard()
    //{
    //    finalScoreText.text = gameScore.ToString();
    //    string score = gameScore.ToString();
    //    if (gameScore < 10)
    //        gameScoreText.text = "0 " + score;
    //    else
    //    {
    //        char[] nums = score.ToCharArray();
    //        gameScoreText.text = nums[0].ToString() + " " + nums[1].ToString();
    //    }
    //}

    //private void RefreshTimeCountdown()
    //{
    //    if (currentMinutes == 0 && currentSeconds < 10)
    //    {
    //        if (currentMinutes == currentSeconds)
    //            timeCountdownText.text = "00.00";
    //        else
    //            timeCountdownText.text = $"0{System.Math.Round(currentSeconds, 2)}";
    //    }
    //    else
    //    {
    //        string minutesStr = currentMinutes.ToString();
    //        string secondsStr = Mathf.Floor(currentSeconds).ToString();

    //        if (currentMinutes < 10)
    //            minutesStr = "0" + minutesStr;
    //        if (currentSeconds < 10)
    //            secondsStr = "0" + secondsStr;
    //        timeCountdownText.text = $"{minutesStr}:{secondsStr}";
    //    }
    //}
}
