using UnityEngine;

namespace BehaviorTree
{
    public class ChunkSpawner : Node
    {
        [SerializeField, Tooltip("The chunk prefab to spawn.")]
        GameObject chunkToSpawn;

        [SerializeField, Tooltip("The range of Y position for the chunk."), Min(0)]
        int deltaYRange = 1;

        [SerializeField, Tooltip("How many copies of the chunk to be spawned in a row."), Min(1)]
        uint copies = 1;

        [SerializeField, Tooltip("Make the copies amount random (from 1 to copies).")]
        bool randomizeCopies = false;

        public override bool Execute(ref GeneratorData data)
        {
            if (chunkToSpawn == null)
            {
                Debug.LogError("Chunk prefab is not assigned.");
                return false;
            }

            float randomValue = (float)data.Randomizer.NextDouble();
            Vector3 offset = Vector2.up * Mathf.Round((randomValue * 2 - 1f) * deltaYRange);

            // Calculate the new position with the offset
            Vector3 newPosition = data.NextLocation.position + offset;

            // Clamp the Y position between -10 and 10
            newPosition.y = Mathf.Clamp(newPosition.y, -10f, 10f);

            copies = randomizeCopies ? data.Randomizer.Next(1, copies + 1) : copies;
            for (int i = 0; i < copies; i++)
            {
                // Apply the Y offset to the current position
                Vector3 spawnPosition = data.NextLocation.position + offset;
                spawnPosition.y = Mathf.Clamp(spawnPosition.y, -10f, 10f);

                GameObject chunk = Instantiate(chunkToSpawn, spawnPosition, Quaternion.identity);
                chunk.transform.SetParent(null);

                if (chunk.TryGetComponent(out Chunk chunkComponent))
                {
                    data.NextLocation = chunkComponent.NextLocation;
                }
                else
                {
                    Debug.LogError("The instantiated chunk does not have a Chunk component.");
                    return false;
                }
            }

            if (data.PreviousChunks.Count == 2) data.PreviousChunks.Dequeue();
            data.PreviousChunks.Enqueue(chunkToSpawn);

            data.ChunksPlaced++;
            return true;
        }
    }
}