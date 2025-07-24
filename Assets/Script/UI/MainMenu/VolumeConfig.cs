using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeConfig : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider effectsSlider;

    public void ChangeMasterVolume(float volume)
    {
        float normalizedVolume = volume / 10f; // Convert 0-10 to 0-1
        if (normalizedVolume > 0f) mixer.SetFloat("MasterVolume", Mathf.Log10(normalizedVolume) * 20f);
        else mixer.SetFloat("MasterVolume", -80f); // Mute if volume is 0

        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    public void ChangeMusicVolume(float volume)
    {
        float normalizedVolume = volume / 10f; // Convert 0-10 to 0-1
        if (normalizedVolume > 0f) mixer.SetFloat("MusicVolume", Mathf.Log10(normalizedVolume) * 20f);
        else mixer.SetFloat("MusicVolume", -80f); // Mute if volume is 0

        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void ChangeEffectsVolume(float volume)
    {
        float normalizedVolume = volume / 10f; // Convert 0-10 to 0-1
        if (normalizedVolume > 0f) mixer.SetFloat("EffectsVolume", Mathf.Log10(normalizedVolume) * 20f);
        else mixer.SetFloat("EffectsVolume", -80f); // Mute if volume is 0

        PlayerPrefs.SetFloat("EffectsVolume", volume);
        PlayerPrefs.Save();
    }

    void Awake()
    {
        float masterVolume, musicVolume, effectsVolume;

        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            ChangeMasterVolume(masterVolume);
            masterSlider.value = masterVolume;
        }
        else
        {
            ChangeMasterVolume(5f); // Default to middle value (5 out of 10)
            masterSlider.value = 5f;
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            ChangeMusicVolume(musicVolume);
            musicSlider.value = musicVolume;
        }
        else
        {
            ChangeMusicVolume(5f); // Default to middle value (5 out of 10)
            musicSlider.value = 5f;
        }

        if (PlayerPrefs.HasKey("EffectsVolume"))
        {
            effectsVolume = PlayerPrefs.GetFloat("EffectsVolume");
            ChangeEffectsVolume(effectsVolume);
            effectsSlider.value = effectsVolume;
        }
        else
        {
            ChangeEffectsVolume(5f); // Default to middle value (5 out of 10)
            effectsSlider.value = 5f;
        }
    }
}
