using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;    // ���̽��� �ܺο� �����ϱ� ���� ����

// �����ϴ� ���
// 1. ������ �����Ͱ� ����
// 2. �����͸� ���̽����� ��ȯ
// 3. ���̽��� �ܺο� ����

// �ҷ����� ���
// 1. �ܺο� ����� ���̽��� ������
// 2. ���̽��� ������ ���·� ��ȯ
// 3. �ҷ��� �����͸� ���

public class PlayerData   // 1. ������ �����Ͱ� ����
{
    // �̸�, ����, ����, �������� ����
    public string name;
    public int level = 1;
    public int coin = 100;
    public int item = -1;
}

public class GameManager : Singleton<GameManager>
{
    public PlayerData nowPlayer = new PlayerData();

    public string path;
    public int nowSlot;

    public bool newStart;
    
    private int stage = 1 , breakCount;
    public float maxHP = 100f;
    public float currentHP = 80f;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        // ���ǿ��� ��õ�� ���
        // ����Ƽ���� �˾Ƽ� �������ִ� ����
        path = Application.persistentDataPath + "/";
    }

    private void Start()
    {
        // GameScene이면 BrickSecene 불러오기
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            LoadBrickScene();
        }

        // GameScene이면 UISecene 불러오기
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            LoadUIScene();
        }


    }

    public void SaveData()
    {
        // 2. �����͸� ���̽����� ��ȯ
        string data = JsonUtility.ToJson(nowPlayer);

        // 3. ���̽��� �ܺο� ����
        // "path" ����� �ش� ���Թ�ȣ�� "data"�� ����
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void LoadData()
    {
        // 1. �ܺο� ����� ���̽��� ������
        string data = File.ReadAllText(path + nowSlot.ToString());

        // 2. ���̽��� ������ ���·� ��ȯ
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

    public void DataClear() // ���� ������ �ʱ�ȭ
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }

    private void LoadBrickScene()
    {
        // BrickScene 로드 함수
        if (!IsSceneLoaded("BrickScene"))
        {
            SceneManager.LoadSceneAsync("BrickScene", LoadSceneMode.Additive);
        }
    }

    private void LoadUIScene()
    {
        // UIScene 로드 함수
        if (!IsSceneLoaded("UIScene"))
        {
            SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);
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