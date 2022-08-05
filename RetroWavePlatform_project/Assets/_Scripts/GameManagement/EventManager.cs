using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent<int, int> OnHeartBonusTook =
        new UnityEvent<int, int>();
    public static UnityEvent<int> OnCoinBonusTook =
        new UnityEvent<int>();
    public static UnityEvent<int, float, float> OnSpeedBonusTook =
        new UnityEvent<int, float, float>();
    public static UnityEvent<int, float> OnImmortalBonusTook =
        new UnityEvent<int, float>();
}
