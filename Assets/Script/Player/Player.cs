using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerData data { get; private set; }

    public static UnityEvent OnPlayerDeath = new();

    public float Energy = 100;
    public float Distance = 0;
    public float Score = 0;
    public float Speed = 0;

    public float TimeStarted = 0;
    public float TimeFinished = 0;
    public float Playtime => TimeFinished - TimeStarted;

    public bool IsStarted = false;
    public static Player Instance { get; private set; }

    [HideInInspector] public float max_speed_modifier = 0;

    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        // Application.targetFrameRate = 60;
        TimeStarted = Time.time;

        OnPlayerDeath.AddListener(PlayerDeath);
    }

    void PlayerDeath()
    {
        OnPlayerDeath.RemoveListener(PlayerDeath);
    }
    
}
