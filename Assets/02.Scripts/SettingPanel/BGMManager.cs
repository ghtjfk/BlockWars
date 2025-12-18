using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    public static float bgmVolume = 0.5f;

    public AudioSource audioSource;

    public AudioClip startBGM;
    public AudioClip mapBGM;
    public AudioClip battleBGM;
    public AudioClip endingSound;

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
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

    void Update()
    {
        audioSource.volume = bgmVolume;

        if (GameManager.Instance.isPause)
        {
            if (audioSource.isPlaying)
                audioSource.Pause();
        }
        else
        {
            if (!audioSource.isPlaying)
                audioSource.UnPause();
        }
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
        else if (sceneName.Contains("EndingScene"))
            clip = endingSound;

        if (clip == null) return;

        if (audioSource.clip == clip) return;
        audioSource.loop = !sceneName.Contains("EndingScene");
        audioSource.clip = clip;
        audioSource.Play();
    }
}
