using UnityEngine;

namespace StateMachine
{
    public abstract class State : MonoBehaviour
    {
        public abstract State Execute(ref GeneratorData data);
    }
}