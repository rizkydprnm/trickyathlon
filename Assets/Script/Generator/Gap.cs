using UnityEngine;

public class Gap : Chunk
{
    void Awake()
    {
        // Calculate gap length and adjust the NextLocation transform directly
        float randomValue = (float)Generator.GetData().Randomizer.NextDouble();
        int gapLength = Mathf.RoundToInt(0.5f * (Player.Instance.data.MAX_SPEED + Player.Instance.MaxSpeedModifier) * randomValue);

        // Move the NextLocation transform to create the gap
        NextLocation.position += Vector3.right * gapLength;
    }
}