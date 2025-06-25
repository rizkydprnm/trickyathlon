using UnityEngine;
using System.Linq;

namespace StateMachine
{
    public class ChunkSpawner : State
    {
        [SerializeField, Tooltip("The chunk prefab to spawn.")]
        GameObject chunkToSpawn;

        [SerializeField, Tooltip("The range of Y position for the chunk."), Min(0)]
        int deltaYRange = 1;

        [SerializeField, Tooltip("How many copies of the chunk to be spawned in a row."), Min(1)]
        int copies = 1;

        [SerializeField, Tooltip("Make the copies amount random (from 1 to copies).")]
        bool randomizeCopies = false;

        [SerializeField, Tooltip("The next state to transition to after spawning a chunk.")]
        State nextState;

        enum CheckType
        {
            Blacklist,
            Whitelist
        }

        [SerializeField, Tooltip("Specify if the check is a blacklist or whitelist.")]
        CheckType checkType = CheckType.Blacklist;

        [SerializeField, Tooltip("List of chunks that are blacklisted or whitelisted.")]
        GameObject[] previousChunks;

        public override State Execute(ref GeneratorData data)
        {
            if (checkType == CheckType.Blacklist)
            {
                // Check if any chunk in the queue is in the blacklist
                if (data.PreviousChunks.Any(chunk => previousChunks.Contains(chunk)))
                {
                    Debug.LogWarning("A previous chunk is blacklisted.");
                    return null;
                }
            }
            else // Whitelist
            {
                // Check if at least one chunk in the queue is in the whitelist
                if (!data.PreviousChunks.Any(chunk => previousChunks.Contains(chunk)))
                {
                    Debug.LogWarning("None of the previous chunks are whitelisted.");
                    return null;
                }
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
                GameObject chunk = Instantiate(chunkToSpawn, newPosition, Quaternion.identity);
                chunk.transform.SetParent(null);

                if (chunk.TryGetComponent(out Chunk chunkComponent))
                {
                    data.NextLocation = chunkComponent.NextLocation;
                }
            }
            if (data.PreviousChunks.Count == 2) data.PreviousChunks.Dequeue();
            data.PreviousChunks.Enqueue(chunkToSpawn);

            data.ChunksPlaced++;
            return nextState;
        }
    }
}