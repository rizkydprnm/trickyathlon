using UnityEngine;
using PrimeTween;

public class Splash : MonoBehaviour
{
    [SerializeField] GameObject[] splashLines;

    void Start()
    {
        Sequence.Create()
            .ChainDelay(0.5f)
            .ChainCallback(() => splashLines[1].SetActive(true))
            .ChainDelay(0.5f)
            .ChainCallback(() => splashLines[2].SetActive(true))
            .ChainDelay(1f)
            .ChainCallback(() => splashLines[3].SetActive(true));
    }
}
