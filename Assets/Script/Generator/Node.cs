using UnityEngine;

namespace BehaviorTree
{
    public abstract class Node: MonoBehaviour
    {
        public abstract bool Execute(ref GeneratorData data);
    }
}