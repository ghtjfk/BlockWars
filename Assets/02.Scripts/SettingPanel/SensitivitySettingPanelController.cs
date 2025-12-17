using UnityEngine;
using UnityEngine.UI;
public class SensitivitySettingPanelController : MonoBehaviour
{
    public GameObject sensitivitySettingPanel;

    public void OpenSensitivitySetting()
    {
        sensitivitySettingPanel.SetActive(true);
    }

    public void CloseSensitivitySetting()
    {
        sensitivitySettingPanel.SetActive(false);
    }
}
