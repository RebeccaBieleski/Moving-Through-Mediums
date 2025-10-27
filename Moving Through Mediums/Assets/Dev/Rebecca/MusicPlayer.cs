using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;

    [SerializeField] private AudioSource musicPlayer;

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        if (!musicPlayer.isPlaying) {
            musicPlayer.Play();
        }
    }
}
