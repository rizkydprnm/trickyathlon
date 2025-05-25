using UnityEngine;

namespace StateMachine
{
    public class ExecuteRandom : State
    {
        [Tooltip("Executes a random child state from this current state.")]
        [SerializeField] State[] nextStates;

        public override State Execute(ref GeneratorData data)
        {
            // Generate a random value between 0 and 1
            float randomValue = (float)data.Randomizer.NextDouble();
            int selectedIndex = Mathf.FloorToInt(randomValue * nextStates.Length);

            return nextStates[selectedIndex].Execute(ref data);
        }
    }
}