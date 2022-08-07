using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent<int> OnLevelScoreChanged =
        new UnityEvent<int>();
    public static UnityEvent<int> OnPlayerHealthChanged =
        new UnityEvent<int>();
    public static UnityEvent<float, float> OnPlayerSpeedChanged =
        new UnityEvent<float, float>();
    public static UnityEvent<float> OnImmortalStatusChanged =
        new UnityEvent<float>();

    public static UnityEvent<int, Vector2> OnDamageReceived =
        new UnityEvent<int, Vector2>();
    public static UnityEvent OnPlayerDied =
        new UnityEvent();
    public static UnityEvent OnLevelLoseEnded =
        new UnityEvent();
    public static UnityEvent OnLevelWinEnded =
        new UnityEvent();
}
