using UnityEngine;

namespace BehaviorTree
{
    public class Sequence : Node
    {
        [SerializeField] bool keepExecuting = false;
        public override bool Execute(ref GeneratorData data)
        {
            foreach (Transform child in transform)
            {
                Node task = child.GetComponent<Node>();
                if (task == null)
                {
                    Debug.LogError($"Child {child.name} does not have a Node component.");
                    continue;
                }

                bool result = task.Execute(ref data);
                if (!result && !keepExecuting)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
