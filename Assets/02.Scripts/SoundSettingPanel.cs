using UnityEngine;
using UnityEngine.UI;

public class SoundSettingPanel : MonoBehaviour
{
    public Slider soundSlider;

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);

        soundSlider.value = savedVolume;

        AudioListener.volume = savedVolume;

        soundSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;

        PlayerPrefs.SetFloat("MasterVolume", value);
        PlayerPrefs.Save();
    }
}
