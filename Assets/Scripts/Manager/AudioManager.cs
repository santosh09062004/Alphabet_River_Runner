using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Clips")]
    public AudioClip jumpClip;
    public AudioClip coinClip;
    public AudioClip correctClip;
    public AudioClip wrongClip;
    public AudioClip backgroundMusic;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (backgroundMusic != null && musicSource != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayJump()
    {
        if (jumpClip != null && sfxSource != null)
            sfxSource.PlayOneShot(jumpClip);
    }

    public void PlayCoin()
    {
        if (coinClip != null && sfxSource != null)
            sfxSource.PlayOneShot(coinClip);
    }

    public void PlayCorrect()
    {
        if (correctClip != null && sfxSource != null)
            sfxSource.PlayOneShot(correctClip);
    }

    public void PlayWrong()
    {
        if (wrongClip != null && sfxSource != null)
            sfxSource.PlayOneShot(wrongClip);
    }
}