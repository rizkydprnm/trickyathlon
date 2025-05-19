using UnityEngine;

namespace BehaviorTree
{
    public class DistanceTest : Node
    {
        [Tooltip("The minimum distance to check."), Min(0), SerializeField]
        float minDistance = 0;

        [Tooltip("The maximum distance to check."), Min(0), SerializeField]
        float maxDistance = 0;

        public override bool Execute(ref GeneratorData data)
        {
            return data.NextLocation.position.x >= minDistance && data.NextLocation.position.x <= maxDistance;
        }
    }
}