using UnityEngine;
using UnityEngine.UI;
public class DescriptionPanelController : MonoBehaviour
{
    public GameObject descriptionPanel;

    public void OpenDescription()
    {
        descriptionPanel.SetActive(true);
    }

    public void CloseDescription()
    {
        descriptionPanel.SetActive(false);
    }
}
