using UnityEngine;
using System.Linq;

namespace BehaviorTree
{
    public class PreviousChunkTest : Node
    {
        enum CheckType
        {
            Blacklist,
            Whitelist
        }

        [SerializeField, Tooltip("Specify if the check is a blacklist or whitelist.")]
        CheckType checkType = CheckType.Blacklist;

        [SerializeField, Tooltip("List of chunks that are blacklisted or whitelisted.")]
        GameObject[] previousChunks;

        public override bool Execute(ref GeneratorData data)
        {
            if (checkType == CheckType.Blacklist)
            {
                // Check if any chunk in the queue is in the blacklist
                if (data.PreviousChunks.Any(chunk => previousChunks.Contains(chunk)))
                {
                    Debug.LogWarning("A previous chunk is blacklisted.");
                    return false;
                }
            }
            else // Whitelist
            {
                // Check if at least one chunk in the queue is in the whitelist
                if (!data.PreviousChunks.Any(chunk => previousChunks.Contains(chunk)))
                {
                    Debug.LogWarning("None of the previous chunks are whitelisted.");
                    return false;
                }
            }

            return true;
        }
    }
}