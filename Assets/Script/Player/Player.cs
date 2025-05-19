using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerData data { get; private set; }

    public static UnityEvent onPlayerDeath = new();

    public static float energy = 100;
    public static float distance = 0;
    public static float score = 0;
    public static Player instance;

    public static bool isStarted = false;

    [HideInInspector] public float max_speed_modifier = 0;

    void Start()
    {
        // Application.targetFrameRate = 60;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
