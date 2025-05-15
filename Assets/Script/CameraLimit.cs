using UnityEngine;

public class CameraLimit : MonoBehaviour
{
    private Transform target;

    void Start()
    {
        target = transform.parent;
        transform.parent = null;
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
    }
}
