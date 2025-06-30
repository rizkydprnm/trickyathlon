using UnityEngine;

namespace StateMachine
{
    public class ExecuteRandom : State
    {
        [Tooltip("The amount of attempts to execute a random state.")]
        [SerializeField, Min(1)] uint attempts = 1;

        [Tooltip("Executes a random child state from this current state.")]
        [SerializeField] State[] nextStates;

        public override State Execute(ref GeneratorData data)
        {
            for (int i = 0; i < attempts; i++)
            {
                // Use the centralized random index selection
                int selectedIndex = Generator.GetRandomIndex(nextStates.Length);

                State result = nextStates[selectedIndex].Execute(ref data);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
    }
}