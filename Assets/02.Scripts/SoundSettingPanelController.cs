using UnityEngine;
using UnityEngine.UI;

public class SoundSettingPanelController : MonoBehaviour
{
    public GameObject soundSettingPanel;

    public AudioSource sampleMusic;

    public void OpenSoundSetting()
    {
        soundSettingPanel.SetActive(true);
    }

    public void CloseSoundSetting()
    {
        soundSettingPanel.SetActive(false);
    }

    public void PlaySampleMusic()
    {
        sampleMusic.Play();
    }
}
