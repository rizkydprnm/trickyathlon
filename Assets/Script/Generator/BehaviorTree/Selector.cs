using UnityEngine;

namespace BehaviorTree
{
    public class Selector : Node
    {
        [Tooltip("Number of random attempts to make. If 0, executes children sequentially. If 1 or more, selects children randomly with that many attempts.")]
        [SerializeField] uint maxRandomAttempts = 0;

        public override bool Execute(ref GeneratorData data)
        {
            if (maxRandomAttempts == 0)
            {
                // Sequential execution
                foreach (Transform child in transform)
                {
                    child.TryGetComponent(out Node task);
                    if (task == null)
                    {
                        Debug.LogError($"Child {child.name} does not have a Node component.");
                        continue;
                    }
                    if (task.Execute(ref data))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                // Random execution with attempts
                for (int attempts = 0; attempts < maxRandomAttempts; attempts++)
                {
                    int selectedIndex = Generator.GetRandomIndex(transform.childCount);
                    transform.GetChild(selectedIndex).TryGetComponent(out Node selectedChild);

                    if (selectedChild == null)
                    {
                        Debug.LogError($"Child at index {selectedIndex} does not have a Node component.");
                        continue;
                    }
                    if (selectedChild.Execute(ref data))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}