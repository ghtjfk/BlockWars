using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int stage = 1 , breakCount;

    void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // GameScene이면 BrickSecene 불러오기
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            LoadBrickScene();
        }
    }

    private void LoadBrickScene()
    {
        // BrickScene 로드 함수
        if (!IsSceneLoaded("BrickScene"))
        {
            SceneManager.LoadSceneAsync("BrickScene", LoadSceneMode.Additive);
        }
    }


    private bool IsSceneLoaded(string sceneName)
    // 씬이 로드되어 있는지 확인하는 함수
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
    //
    // stage 관련 함수
    //
    public void setStage(int stage)
    {
        this.stage = stage;
    }
    public int getStage()
    {
        return stage;
    }
    public void initStage()
    {
        stage = 0;
    }




    //
    // break block count 관련 함수
    //
    public void increaseBreakBlockCount()
    {
        breakCount++;
    }
    

    public void initBreakBlockCount()
    {
        breakCount = 0;
    }
}