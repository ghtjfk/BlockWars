using UnityEngine;
using UnityEngine.UI;   

public class DescriptionPageController : MonoBehaviour
{
    public GameObject[] Descriptionpages;
    private int currentIndex = 0;

    public GameObject PrevButton;
    public GameObject NextButton;

    void OnEnable()
    {
        ShowPage(0);
    }

    void ShowPage(int index)
    {
        for (int i = 0; i < Descriptionpages.Length; i++)
        {
            Descriptionpages[i].SetActive(i == index);
        }

        currentIndex = index;
    }

    public void NextPage()
    {
        if (currentIndex < Descriptionpages.Length - 1)
        {
            ShowPage(currentIndex + 1);
            NextButton.SetActive(true);
        }
        else
            NextButton.SetActive(false);
    }

    public void PrevPage()
    {
        if (currentIndex > 0)
        {
            ShowPage(currentIndex - 1);
            PrevButton.SetActive(true);
        }
        else
            PrevButton.SetActive(false);
    }
}
