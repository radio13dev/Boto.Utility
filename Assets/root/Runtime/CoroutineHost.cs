using UnityEngine;

public class CoroutineHost : MonoBehaviour
{
    public static CoroutineHost Instance;
    
    public void Awake()
    {
        Instance = this;
    }
}