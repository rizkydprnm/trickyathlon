using UnityEngine;

namespace BehaviorTree
{
    public class GapSpawner : Node
    {
        [Tooltip("The length of the gap to spawn."), Min(1), SerializeField]
        int gapLength = 1;

        [Tooltip("Specify if the gap length should be randomized."), SerializeField]
        bool randomizeGapLength = false;

        public override bool Execute(ref GeneratorData data)
        {
            if (randomizeGapLength)
            {
                float randomValue = (float)data.Randomizer.NextDouble();
                gapLength = Mathf.RoundToInt(Mathf.Lerp(1, gapLength, randomValue));
            }

            data.NextLocation.position += Vector3.right * gapLength;

            return true;
        }
    }
}