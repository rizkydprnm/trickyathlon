using UnityEngine;

namespace StateMachine
{
    public class GroundStateMachine : MonoBehaviour
    {
        public State CurrentState;

        public void Execute(ref GeneratorData data)
        {
            if (CurrentState == null)
            {
                Debug.LogError("CurrentState is not set in the GroundStateMachine.");
                return;
            }

            // Execute the current state and get the next state
            State nextState = CurrentState.Execute(ref data);

            // Transition to the next state if it's different from the current one
            if (nextState != null && nextState != CurrentState)
            {
                CurrentState = nextState;
            }

        }
    }
}