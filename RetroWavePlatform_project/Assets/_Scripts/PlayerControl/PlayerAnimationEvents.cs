using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public void FinishLoseLevel() =>
        EventManager.OnLevelLoseEnded.Invoke();    
}
