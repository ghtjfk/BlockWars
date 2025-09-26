using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapButtonManager : MonoBehaviour
{
    public Button map1Button;
    public Button map2Button;
    public Button map3Button;
    public Button map4Button;
    public Button map5Button;
    public Button map6Button;
    public Button map7Button;
    public Button map8Button;
    public Button map9Button;
    public Button map10Button;

    void Start()
    {
        map1Button.onClick.AddListener(OnMap1ButtonClick);
        map2Button.onClick.AddListener(OnMap2ButtonClick);
        map3Button.onClick.AddListener(OnMap3ButtonClick);
        map4Button.onClick.AddListener(OnMap4ButtonClick);
    }

    private void OnMap1ButtonClick()
    {
        SceneManager.LoadScene("Stage1");
        SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
    }

    private void OnMap2ButtonClick()
    {
        SceneManager.LoadScene("Stage2");
        SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
    }

    private void OnMap3ButtonClick()
    {
        SceneManager.LoadScene("Stage3");
        SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
    }

    private void OnMap4ButtonClick()
    {
        SceneManager.LoadScene("Stage4");
        SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
    }
}
