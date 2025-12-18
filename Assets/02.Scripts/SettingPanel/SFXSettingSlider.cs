using UnityEngine;
using UnityEngine.UI;

public class SFXSettingSlider : MonoBehaviour
{
    public Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void OnEnable()
    {
        Sync();
    }

    void Sync()
    {
        slider.SetValueWithoutNotify(SFXManager.sfxVolume);
    }

    void OnValueChanged(float value)
    {
        SFXManager.sfxVolume = value;
    }
}
