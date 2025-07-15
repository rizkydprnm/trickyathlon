using UnityEngine;

public class GeneratorTestScene : MonoBehaviour
{
    [SerializeField] GameObject[] objectList;

    void Update()
    {
        if (Player.Instance.Distance >= 50f)
        {
            foreach (GameObject obj in objectList)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
            enabled = false; // Disable this script after execution
        }
    }
}
