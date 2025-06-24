using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Profiling;

using System.Collections.Generic;

using BehaviorTree;
using StateMachine;
using UnityEditor;

/// <summary>
/// GeneratorData struct holds the data needed for the generator to work.
/// </summary>
public struct GeneratorData
{
    public System.Random Randomizer;
    public int Seed;

    public Queue<GameObject> PreviousChunks;
    public Transform NextLocation;

    public int ChunksPlaced;
}

/// <summary>
/// Generator class is responsible for generating chunks in the game world.
/// It uses a behavior tree-like structure to execute its children nodes in a specific order.
/// </summary>

public class Generator : MonoBehaviour
{
    [Tooltip("The starting location for the generator.")]
    [SerializeField] Transform startLocation;

    [Tooltip("The number of chunks to be generated the first time.")]
    [SerializeField, Min(10)] int initialChunksAmount = 10;

    enum Mode
    {
        [InspectorName("Behavior Tree")] BehaviorTree,
        [InspectorName("State Machine")] StateMachine
    }

    [Tooltip("The mode of the generator. Determines how the generator will execute its children nodes.")]
    [SerializeField] Mode mode = Mode.BehaviorTree;

    protected static GeneratorData data;

    public static UnityEvent ChunkDestroyed = new();

    public static void Initialize(int seed)
    {
        data = new();
        
        data.PreviousChunks = new();
        data.Randomizer = new(seed);
        data.Seed = seed;
    }

    void Start()
    {
        if (data.Randomizer == null) Initialize(12345678);
        Debug.Log($"Generator initialized with seed: {data.Seed}");

        ChunkDestroyed.AddListener(SpawnChunk);

        data.NextLocation = startLocation;

        Profiler.BeginSample("Generator Start");
        for (int i = 0; i < initialChunksAmount; i++) SpawnChunk();
        Profiler.EndSample();

#if UNITY_EDITOR
        EditorApplication.isPaused = true; // Pause the editor after initialization for debugging purposes
#endif

    }

    void SpawnChunk()
    {
        switch (mode)
        {
            case Mode.BehaviorTree:
                GetComponent<Node>().Execute(ref data);
                break;
            case Mode.StateMachine:
                GetComponent<GroundStateMachine>().Execute(ref data);
                break;
            default:
                Debug.LogError("Unknown generator mode.");
                break;
        }
    }

    public static GeneratorData GetData()
    {
        return data;
    }
}

