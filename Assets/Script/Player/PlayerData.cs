using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Data")]
public class PlayerData : ScriptableObject
{
    [field: SerializeField, Min(1f)] public float MAX_SPEED { get; private set; } = 10f;
    [field: SerializeField, Min(1f)] public float ACCELERATION { get; private set; } = 5f;

    [field: SerializeField, Min(1f)] public float JUMP_FORCE { get; private set; } = 10f;
}