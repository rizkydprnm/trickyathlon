using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerData data { get; private set; }

    [HideInInspector] public float energy = 100;

    public static float distance = 0;
    public static float score = 0;
    public static bool isStarted = false;
    
    [HideInInspector] public float max_speed_modifier = 0;

    void Start()
    {
        // Application.targetFrameRate = 60;
    }
}
