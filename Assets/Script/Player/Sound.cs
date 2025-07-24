using UnityEngine;

public class Sound : MonoBehaviour
{
    AudioSource stepSound;

    void Awake()
    {
        stepSound = GetComponent<AudioSource>();
    }

    public void PlayStepSound()
    {
        if (stepSound != null && !stepSound.isPlaying)
        {
            stepSound.pitch = Random.Range(0.9f, 1.1f); // Randomize pitch slightly
            stepSound.Play();
        }
    }
}