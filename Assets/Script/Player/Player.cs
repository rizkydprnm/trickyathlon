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

    public float MaxSpeedModifier = 0;

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
    
    public void CheckSpeedMilestones()
    {
        // Calculate the next milestone distance for the current level
        // Pattern: 100, 400, 900, 1600, ...
        // nextMilestone = Mathf.Pow(100 * currentSpeedLevel, 2);
        if (Distance >= nextMilestone)
        {
            MaxSpeedModifier += 1f;

            nextMilestone += (++currentSpeedLevel + 1) * 50f;
            Debug.Log($"{currentSpeedLevel} {nextMilestone}");
        }
    }
}
