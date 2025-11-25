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

public enum TurnState
{
    PlayerTurn,
    MonsterSelect,
    MonsterTurn
}

public class GameManager : Singleton<GameManager>
{
    public TurnState turnState = TurnState.PlayerTurn;

    public PlayerData nowPlayer = new PlayerData();

    public string path;
    public int nowSlot;

    public bool newStart;
    
    private int stage =1 , breakCount;

    public float redMoon;
    public float isredMoonGenerator = -1f;

    public bool isPause = false;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        // 강의에서 추천한 경로
        // 유니티에서 알아서 생성해주는 폴더
        path = Application.persistentDataPath + "/";

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // 메모리 누수 방지용: 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("StartScene");
            Debug.Log(Application.persistentDataPath);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.StartsWith("GameScene"))
        {

            Debug.Log("Stage씬 감지됨 → BrickScene / UIScene 로드 시도");
            LoadBrickScene();
            LoadUIScene();
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
        // BrickScene 로드 함수
        if (!IsSceneLoaded("BrickScene"))
        {
            SceneManager.LoadSceneAsync("BrickScene", LoadSceneMode.Additive);
        }
    }

    public void LoadUIScene()
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

    public int getBreakBlockCount()
    {
        return breakCount;
    }

    public void NextTurn()
    {
        switch (turnState)
        {
            case TurnState.PlayerTurn:
                if (ModeSwitcher.Instance.GetCurrentMode())
                {
                    turnState = TurnState.MonsterTurn;
                    StartCoroutine(MonsterManager.Instance.OnMonsterTurnStart());
                    break;
                }
                turnState = TurnState.MonsterSelect;
                ModeSwitcher.Instance.DecreaseCooldown();
                break;
            case TurnState.MonsterSelect:
                turnState = TurnState.MonsterTurn;
                //코루틴은 아래 방식으로 호출해야함
                StartCoroutine(MonsterManager.Instance.OnMonsterTurnStart());
                break;
            case TurnState.MonsterTurn:
                turnState = TurnState.PlayerTurn;
                break;
        }
    }

    public void initTurn()
    {
        turnState = TurnState.PlayerTurn;
    }


}