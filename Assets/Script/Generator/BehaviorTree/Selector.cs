using UnityEngine;

namespace BehaviorTree
{
    public class Selector : Node
    {
        [Tooltip("If enabled, the selector will choose children randomly. Otherwise, it will execute them sequentially.")]
        [SerializeField] bool randomSelector;

        [Tooltip("The maximum number of attempts to select a random child when using random selection.")]
        [SerializeField, Min(1)] int maxRandomAttempts = 1;

        public override bool Execute(ref GeneratorData data)
        {
            if (!randomSelector)
            {
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
            else {
                int attempts = 0;
                while (attempts < maxRandomAttempts)
                {
                    int selectedIndex = Generator.GetRandomIndex(transform.childCount);
                    transform.GetChild(selectedIndex).TryGetComponent(out Node selectedChild);

                    if (selectedChild == null)
                    {
                        Debug.LogError($"Child {selectedChild.name} does not have a Node component.");
                        continue;
                    }
                    if (selectedChild.Execute(ref data))
                    {
                        return true;
                    }
                    attempts++;
                }
                return false;
            }
        }
    }
}