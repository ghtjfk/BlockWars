using UnityEngine;
using UnityEngine.UI;

public class SensitivitySettingSlider : MonoBehaviour
{
    public BallMoveing ball;

    public void SettingSensitivity(float value)
    {
        ball.sensitivity = value;
    }
}
