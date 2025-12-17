using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour
{
    public Slider slider;
    private BallMoveing ball;

    void Start()
    {
        ball = FindObjectOfType<BallMoveing>();

        slider.value = ball.sensitivity;

        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    void OnSliderChanged(float value)
    {
        ball.sensitivity = value;
    }
}
