using UnityEngine;
using BehaviorTree;

public class Gap : Node
{
    [Tooltip("The length of the gap to be generated.")]
    [SerializeField, Min(1)] int length = 5;

    [Tooltip("The gap will be generated after this many chunks.")]
    [SerializeField, Min(1)] int spawnAfter = 0;

    [Tooltip("If enabled, the gap length will be random between 0 and the specified length.")]
    [SerializeField] bool randomLength = false;

    [SerializeField, Range(0f, 1f)] float chance = 0.25f;

    public override bool Execute(ref GeneratorData data)
    {
        if (data.ChunksPlaced < spawnAfter) return false;
        if (data.Randomizer.NextDouble() >= chance)
        {
            chance = Mathf.Clamp01(chance + 0.01f);
            return false;
        }

        chance = Mathf.Clamp01(chance - 0.1f);
        if (randomLength) data.NextLocation.position += Vector3.right * (data.Randomizer.Next(1, length + 1));
        else data.NextLocation.position += Vector3.right * length;

        return true;
    }
}
