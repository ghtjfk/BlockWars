using UnityEngine;
using UnityEngine.UI;

public class BGMSlider : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider.value = BGMManager.bgmVolume;
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(float value)
    {
        BGMManager.bgmVolume = value;
    }
}
