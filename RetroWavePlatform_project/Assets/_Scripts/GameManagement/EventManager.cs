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

    public static UnityEvent<int> OnDamageDone =
        new UnityEvent<int>();

}
