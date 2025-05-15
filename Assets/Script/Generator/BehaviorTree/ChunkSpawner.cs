using UnityEngine;
using System.Linq;
using BehaviorTree;

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

        float randomValue = (float)data.Randomizer.NextDouble();
        Vector3 offset = Vector2.up * Mathf.Round((randomValue * 2 - 1f) * chunkData.DeltaYRange);
        
        // Calculate the new position with the offset
        Vector3 newPosition = data.NextLocation.position + offset;
        
        // Clamp the Y position between -10 and 10
        newPosition.y = Mathf.Clamp(newPosition.y, -10f, 10f);

        GameObject chunk = Instantiate(chunkData.ChunkPrefab, newPosition, Quaternion.identity);
        chunk.transform.SetParent(null);

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