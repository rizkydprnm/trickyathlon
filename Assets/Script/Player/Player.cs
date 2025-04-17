using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerData data { get; private set; }

    [HideInInspector] public float energy = 100;
    [HideInInspector] public float distance = 0;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void FixedUpdate()
    {
    }
}
