using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Profiling;

using System.Collections.Generic;

using BehaviorTree;
using StateMachine;
using UnityEditor;

public struct GeneratorData
{
    public System.Random Randomizer;
    public int Seed;

    public Queue<GameObject> PreviousChunks;
    public Transform NextLocation;

    public int ChunksPlaced;
}

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

        data.Randomizer = new(seed);
        data.Seed = seed;
        data.PreviousChunks = new();
        data.NextLocation = null; // Will be set in Start
        data.ChunksPlaced = 0;
    }

    void Start()
    {
        // Initialize(0x3ef511d7); // Default seed, can be changed later
        Debug.Log($"Generator initialized with seed: {data.Seed}");

        ChunkDestroyed.AddListener(SpawnChunk);

        data.NextLocation = startLocation;

        if (mode == Mode.BehaviorTree)
        {
            Profiler.BeginSample("Generator BT");
            while (data.ChunksPlaced < initialChunksAmount)
                GetComponent<Node>().Execute(ref data);
            Profiler.EndSample();
        }
        else if (mode == Mode.StateMachine)
        {
            Profiler.BeginSample("Generator SM");
            while (data.ChunksPlaced < initialChunksAmount)
                GetComponent<GroundStateMachine>().Execute(ref data);
            Profiler.EndSample();
        }
        else
        {
            Debug.LogError("Unknown generator mode.");
        }

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

    // Centralized random index selection to ensure consistent behavior between BehaviorTree and StateMachine
    public static int GetRandomIndex(int count)
    {
        float randomValue = (float)data.Randomizer.NextDouble();
        return Mathf.FloorToInt(randomValue * count);
    }
}