using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private int stage, breakCount;

    void Awake()
    {
        // �̱��� ���� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // ���� �� �̸��� MapScene�� ���� BrickScene�� �߰��� �ε�
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            LoadBrickScene();
        }
    }

    private void LoadBrickScene()
    {
        // BrickScene�� �̹� �ε�Ǿ� ���� �ʴٸ� Additive �ε�
        if (!IsSceneLoaded("BrickScene"))
        {
            SceneManager.LoadSceneAsync("BrickScene", LoadSceneMode.Additive);
        }
    }

    private bool IsSceneLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}