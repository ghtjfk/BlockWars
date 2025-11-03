using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;    // 제이슨을 외부에 저장하기 위해 존재

// 저장하는 방법
// 1. 저장할 데이터가 존재
// 2. 데이터를 제이슨으로 변환
// 3. 제이슨을 외부에 저장

// 불러오는 방법
// 1. 외부에 저장된 제이슨을 가져옴
// 2. 제이슨을 데이터 형태로 변환
// 3. 불러온 데이터를 사용

public class PlayerData   // 1. 저장할 데이터가 존재
{
    // 이름, 레벨, 코인, 착용중인 무기
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

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        // 강의에서 추천한 경로
        // 유니티에서 알아서 생성해주는 폴더
        path = Application.persistentDataPath + "/";
    }

    private void Start()
    {
        // 현재 씬 이름이 MapScene일 때만 BrickScene을 추가로 로드
        if (SceneManager.GetActiveScene().name == "MapScene")
        {
            LoadBrickScene();
        }
    }

    public void SaveData()
    {
        // 2. 데이터를 제이슨으로 변환
        string data = JsonUtility.ToJson(nowPlayer);

        // 3. 제이슨을 외부에 저장
        // "path" 경로의 해당 슬롯번호에 "data"를 저장
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void LoadData()
    {
        // 1. 외부에 저장된 제이슨을 가져옴
        string data = File.ReadAllText(path + nowSlot.ToString());

        // 2. 제이슨을 데이터 형태로 변환
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

    public void DataClear() // 슬롯 데이터 초기화
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
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