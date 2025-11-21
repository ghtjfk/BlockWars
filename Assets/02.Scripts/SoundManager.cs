using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmSource;
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.value = bgmSource.volume;
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    void ChangeVolume(float value)
    {
        bgmSource.volume = value;
    }
}
