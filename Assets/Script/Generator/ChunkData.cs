using UnityEngine;

[CreateAssetMenu(fileName = "ChunkData", menuName = "Chunk Data", order = 1)]
public class ChunkData : ScriptableObject
{
    [Tooltip("GameObject to spawn. Should include 2 gameobjects at both ends to allow for chunks to be connected.")]
    public GameObject ChunkPrefab;
    
    [Tooltip("If the chunks before is in this list, this chunk will/won't be spawned.")]
    public ChunkData[] PreviousChunksList;

    [Tooltip("If true, the list above acts as a whitelist (will spawn if); otherwise, it acts as a blacklist (won't spawn if).")]
    public bool ListAsWhiteList = false;

    [Tooltip("The random Y distance from the previous chunk to the next chunk.")]
    [Min(0)] public int DeltaYRange;

    [Tooltip("The chunk will be spawned after this many chunks.")]
    [Min(0)] public int SpawnAfter = 0;
}