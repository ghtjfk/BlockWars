using UnityEngine;
using UnityEngine.UI;

public class BGMSlider : MonoBehaviour
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

    void Start()
    {
        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener(OnValueChanged);

        Sync();
    }

    void Sync()
    {
        slider.SetValueWithoutNotify(BGMManager.bgmVolume);
    }

    void OnValueChanged(float value)
    {
        BGMManager.bgmVolume = value;
    }
}
