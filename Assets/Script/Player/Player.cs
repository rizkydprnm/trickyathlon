using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerData data { get; private set; }

    public static UnityEvent onPlayerDeath = new();

    [HideInInspector] public float energy = 100;

    public static float distance = 0;
    public static float score = 0;
    public static bool isStarted = false;

    [HideInInspector] public float max_speed_modifier = 0;

    void Start()
    {
        // Application.targetFrameRate = 60;
    }

    void Update()
    {
        OutOfBounds();
    }

    void OutOfBounds()
    {
        if (transform.position.y < -25)
        {
            onPlayerDeath.Invoke();
            Destroy(gameObject);
        }
    }
}
