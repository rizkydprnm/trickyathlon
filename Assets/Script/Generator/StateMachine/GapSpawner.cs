using UnityEngine;

namespace StateMachine
{
    public class GapSpawner : State
    {
        [Tooltip("The length of the gap to spawn."), Min(1), SerializeField]
        int gapLength = 1;

        [Tooltip("Specify if the gap length should be randomized."), SerializeField]
        bool randomizeGapLength = false;

        [Tooltip("The next state to execute after this action."), SerializeField]
        State nextState;

        public override State Execute(ref GeneratorData data)
        {
            if (randomizeGapLength)
            {
                float randomValue = (float)data.Randomizer.NextDouble();
                gapLength = Mathf.RoundToInt(Mathf.Lerp(1, gapLength, randomValue));
            }

            data.NextLocation.position += Vector3.right * gapLength;
            return nextState;
        }
    }
}