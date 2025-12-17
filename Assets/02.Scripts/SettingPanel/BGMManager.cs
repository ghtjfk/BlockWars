using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    public static float bgmVolume = 1.0f;

    public AudioSource audioSource;

    public AudioClip startBGM;
    public AudioClip mapBGM;
    public AudioClip battleBGM;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayBGM(SceneManager.GetActiveScene().name);

        audioSource.clip = startBGM;
        audioSource.volume = 1f;
        audioSource.Play();
    }

    void Update()
    {
        audioSource.volume = bgmVolume;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGM(scene.name);
    }

    void PlayBGM(string sceneName)
    {
        AudioClip clip = null;

        if (sceneName.Contains("StartScene"))
            clip = startBGM;
        else if (sceneName.Contains("MapScene"))
            clip = mapBGM;
        else if (sceneName.Contains("GameScene"))
            clip = battleBGM;

        if (clip == null) return;

        if (audioSource.clip == clip) return;

        audioSource.clip = clip;
        audioSource.Play();
    }
}
