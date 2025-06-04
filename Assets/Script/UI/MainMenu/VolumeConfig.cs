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
        if (volume > 0f) mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        else mixer.SetFloat("MasterVolume", -80f); // Mute if volume is 0

        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    public void ChangeMusicVolume(float volume)
    {
        if (volume > 0f) mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        else mixer.SetFloat("MusicVolume", -80f); // Mute if volume is 0

        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void ChangeEffectsVolume(float volume)
    {
        if (volume > 0f) mixer.SetFloat("EffectsVolume", Mathf.Log10(volume) * 20);
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
            ChangeMasterVolume(0.5f);
            masterSlider.value = 0.5f;
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            ChangeMusicVolume(musicVolume);
            musicSlider.value = musicVolume;
        }
        else
        {
            ChangeMusicVolume(0.5f);
            musicSlider.value = 0.5f;
        }

        if (PlayerPrefs.HasKey("EffectsVolume"))
        {
            effectsVolume = PlayerPrefs.GetFloat("EffectsVolume");
            ChangeEffectsVolume(effectsVolume);
            effectsSlider.value = effectsVolume;
        }
        else
        {
            ChangeEffectsVolume(0.5f);
            effectsSlider.value = 0.5f;
        }
    }
}
