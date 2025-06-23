using UnityEngine;

namespace BehaviorTree
{
    public class GapSpawner : Node
    {
        public override bool Execute(ref GeneratorData data)
        {

            float randomValue = (float)data.Randomizer.NextDouble();
            int gapLength = Mathf.RoundToInt(0.5f * (Player.Instance.data.MAX_SPEED + Player.Instance.MaxSpeedModifier) * randomValue);

            data.NextLocation.position += Vector3.right * gapLength;

            return true;
        }
    }
}