using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;    // 제이슨을 외부에 저장하기 위해 존재

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
    public float maxHP = 100f;
    public float curruntHP;
    public float attackDamage = 5f;
    public int stage = 1;
    public float hpBonus = 0f; // 최대 HP 증가 보너스
    public float damageBonus = 0f; // 공격력 증가 보너스
}


public class GameManager : Singleton<GameManager>
{    
    public PlayerData nowPlayer = new PlayerData();
    public string path;
    public int nowSlot;

    public bool newStart;
    
    private int breakCount;

    public float redMoon;

    public bool isPause = false;
    public bool isGameOver = false;


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

    public void ApplyShopEffect(string itemName)
    {
        // 아이템 효과 적용 후, 최대 HP와 공격력을 갱신합니다.
    
        if (itemName.Contains("HP UP")) // "HP UP" 아이템 구매 시
        {
            float bonusAmount = 20f; // 한 번 구매 시 HP 20 증가 가정
            nowPlayer.hpBonus += bonusAmount;
        
            // ⭐ 현재 HP도 같이 증가시켜서 구매 즉시 체력 회복 효과를 줍니다.
            // 현재 체력(curruntHP)에 보너스만큼 더하고, 최대 HP를 초과하지 않도록 제한합니다.
            nowPlayer.curruntHP = Mathf.Min(nowPlayer.curruntHP + bonusAmount, 
                                        nowPlayer.maxHP + nowPlayer.hpBonus);
        
            Debug.Log($"HP UP 효과 적용! HP 보너스: {nowPlayer.hpBonus}");
        
            // [TODO] Health UI (HP Bar)를 갱신하는 함수를 여기에 호출해야 합니다.
        }
        else if (itemName.Contains("DMG UP")) // "DMG UP" 아이템 구매 시
        {
            float bonusAmount = 2f; // 한 번 구매 시 공격력 2 증가 가정
            nowPlayer.damageBonus += bonusAmount;
        
            Debug.Log($"DMG UP 효과 적용! 공격력 보너스: {nowPlayer.damageBonus}");
        
            // [TODO] 공격력 관련 UI를 갱신하는 함수를 여기에 호출해야 합니다.
        }
    
        SaveData(); // 변경된 플레이어 데이터를 저장
    }
    public float GetPlayerMaxHP()
    {
        // 기본 HP + 아이템 보너스
        return nowPlayer.maxHP + nowPlayer.hpBonus;
    }
    public float GetPlayerAttackDamage()
    {
        // 기본 공격력 + 아이템 보너스
        return nowPlayer.attackDamage + nowPlayer.damageBonus;
    }

}