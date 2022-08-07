using UnityEngine;

public class DontDestroyScript : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
