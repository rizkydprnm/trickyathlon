using UnityEngine;

[CreateAssetMenu(fileName = "ChunkData", menuName = "Chunk Data", order = 1)]
public class ChunkData : ScriptableObject
{
    [Tooltip("GameObject to spawn. Should include 2 gameobjects at both ends to allow for chunks to be connected.")]
    public GameObject ChunkPrefab;
    
    [Tooltip("Chunks allowed/banned to spawn before this chunk.")]
    public ChunkData[] PreviousChunksList;

    [Tooltip("If true, the list above acts as a whitelist; otherwise, it acts as a blacklist.")]
    public bool ListAsWhiteList = false;

    [Tooltip("The random Y distance from the previous chunk to the next chunk.")]
    [Min(0)] public int DeltaYRange;
}