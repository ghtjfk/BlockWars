using UnityEngine;

public class DescriptionPageController : MonoBehaviour
{
    public GameObject[] Descriptionpages;

    public GameObject PrevButton;
    public GameObject NextButton;

    private int currentIndex = 0;

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

        PrevButton.SetActive(currentIndex > 0);
        NextButton.SetActive(currentIndex < Descriptionpages.Length - 1);
    }

    public void NextPage()
    {
        if (currentIndex < Descriptionpages.Length - 1)
        {
            ShowPage(currentIndex + 1);
        }
    }
    public void PrevPage()
    {
        if (currentIndex > 0)
        {
            ShowPage(currentIndex - 1);
        }
    }
}
