using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerData data { get; private set; }

    public static UnityEvent OnPlayerDeath = new();

    public float Distance = 0;
    public float Speed = 0;

    public float nextMilestone = 100f;
    public int currentSpeedLevel = 1;

    [HideInInspector] public float TimeStarted = 0;
    [HideInInspector] public float TimeFinished = 0;
    [HideInInspector] public float Playtime => TimeFinished - TimeStarted;

    public static Player Instance { get; private set; }

    public float max_speed_modifier = 0;

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
