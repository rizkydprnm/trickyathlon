using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// GeneratorData struct holds the data needed for the generator to work.
/// </summary>
public struct GeneratorData
{
    public System.Random Randomizer;
    public int Seed;
    public float LastRandomValue;

    public ChunkData PreviousChunk;
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

    protected static GeneratorData data;
    protected static bool isDataInitialized = false;

    public static UnityEvent ChunkDestroyed = new();

    public static void Initialize(int seed)
    {
        if (!isDataInitialized)
        {
            data = new();
            isDataInitialized = true;
        }
        data.Randomizer = new(seed);
        data.Seed = seed;
    }

    void Start()
    {
        Initialize(Random.Range(-10000, 10000));
        ChunkDestroyed.AddListener(SpawnChunk);

        data.NextLocation = startLocation;
        for (int i = 0; i < 25; i++) SpawnChunk();
    }

    void SpawnChunk()
    {
        data.LastRandomValue = (float)data.Randomizer.NextDouble();
        GetComponent<Node>().Execute(ref data);
    }
}

