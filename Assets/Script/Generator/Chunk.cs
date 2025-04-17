using UnityEngine;

/// <summary>
/// Represents a chunk in the game world. Automatically destroys itself
/// when it is too far behind the camera to optimize performance.
/// </summary>
public class Chunk : MonoBehaviour
{
    [field: SerializeField] 
    [Tooltip("The transform indicating the next location for chunk placement.")]
    public Transform NextLocation { get; private set; }

    void FixedUpdate()
    {
        // Destroy the chunk if it is too far behind the camera
        if (Camera.main.transform.position.x - transform.position.x > 25f)
        {
            Destroy(gameObject);
            Generator.ChunkDestroyed?.Invoke();
        }
    }
}