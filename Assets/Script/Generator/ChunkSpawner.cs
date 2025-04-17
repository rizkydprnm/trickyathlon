using UnityEngine;
using System.Linq;

public class ChunkSpawner : Node
{
    [SerializeField] ChunkData chunkData;

    public override bool Execute(ref GeneratorData data)
    {
        if (chunkData == null || chunkData.ChunkPrefab == null)
        {
            Debug.LogError("Chunk data or Chunk prefab is not assigned.");
            return false;
        }

        if (chunkData.PreviousChunksList != null && chunkData.PreviousChunksList.Length > 0)
        {
            if (chunkData.ListAsWhiteList)
            {
                bool isValid = chunkData.PreviousChunksList.Contains(data.PreviousChunk);
                if (!isValid) return false;
            }
            else
            {
                if (chunkData.PreviousChunksList.Contains(data.PreviousChunk))
                    return false;
            }
        }

        Vector3 offset = Vector2.up * Mathf.Round((data.LastRandomValue * 2 - 1f) * chunkData.DeltaYRange);

        GameObject chunk = Instantiate(chunkData.ChunkPrefab, data.NextLocation.position + offset, Quaternion.identity);
        chunk.transform.SetParent(null);
        // chunk.transform.localPosition = Vector3.zero;

        Chunk chunkComponent = chunk.GetComponent<Chunk>();
        if (chunkComponent != null)
        {
            data.NextLocation = chunkComponent.NextLocation;
        }
        else
        {
            Debug.LogError("The instantiated chunk does not have a Chunk component.");
            return false;
        }

        data.PreviousChunk = chunkData;
        data.ChunksPlaced++;
        return true;
    }
}