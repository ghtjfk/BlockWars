using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour
{
    public Slider slider;

    void OnEnable()
    {
        slider.SetValueWithoutNotify(SettingsData.sensitivity);
    }

    void Start()
    {
        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(float value)
    {
        SettingsData.sensitivity = value;
    }
}
