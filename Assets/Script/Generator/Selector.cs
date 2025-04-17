using UnityEngine;

/// <summary>
/// Selector node that executes its children in a specific order or randomly.
/// If the selector is set to random, it will attempt to select a child randomly
/// </summary>
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
                Node task = child.GetComponent<Node>();
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
                Node selectedChild = transform.GetChild((int) Mathf.Floor(data.LastRandomValue * transform.childCount)).GetComponent<Node>();
                
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