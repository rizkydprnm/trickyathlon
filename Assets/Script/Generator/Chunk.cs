using UnityEngine;

public class Chunk : MonoBehaviour
{
    [field: SerializeField] 
    [Tooltip("The transform indicating the next location for chunk placement.")]
    public Transform NextLocation { get; private set; }

    void FixedUpdate()
    {
        // Destroy the chunk if it is too far behind the camera
        if (Camera.main.transform.position.x - transform.position.x > 50f)
        {
            Destroy(gameObject);
            Generator.ChunkDestroyed?.Invoke();
        }
    }
}