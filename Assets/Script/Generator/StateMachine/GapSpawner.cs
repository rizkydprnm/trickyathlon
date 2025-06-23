using UnityEngine;

namespace StateMachine
{
    public class GapSpawner : State
    {
        [Tooltip("The next state to execute after this action."), SerializeField]
        State nextState;

        public override State Execute(ref GeneratorData data)
        {
            float randomValue = (float)data.Randomizer.NextDouble();
            int gapLength = Mathf.RoundToInt(0.5f * (Player.Instance.data.MAX_SPEED + Player.Instance.MaxSpeedModifier) * randomValue);

            data.NextLocation.position += Vector3.right * gapLength;
            return nextState;
        }
    }
}