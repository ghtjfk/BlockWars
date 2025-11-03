using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private int stage, breakCount;

    void Awake()
    {
        // 싱글톤 패턴 구현
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
        // 현재 씬 이름이 MapScene일 때만 BrickScene을 추가로 로드
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            LoadBrickScene();
        }
    }

    private void LoadBrickScene()
    {
        // BrickScene이 이미 로드되어 있지 않다면 Additive 로드
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