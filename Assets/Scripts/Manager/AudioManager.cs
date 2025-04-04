using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SfxSource;

    [Header("---------------Background Music---------------")]
    public AudioClip backgroundMusic;


    [Header("---------------Player's Audio Clips---------------")]
    public AudioClip playerDeadByJumpSFX;
    public AudioClip playerCamouflageSFX;
    public AudioClip playerDashingSFX;
    public List<AudioClip> playerDeadByPoisonSFX;
    public List<AudioClip> playerDeadByEnemySFX;
    public List<AudioClip> playerDeadByElectricSFX;
    public List<AudioClip> playerDeadByFireSFX;
    public List<AudioClip> playerJumpSFX;
    public List<AudioClip> playerDashDoneSFX;
    public List<AudioClip> playerRevivalSFX;



    [Header("---------------Shuriken Sound Effects---------------")]
    public List<AudioClip> shurikenHitEnemySFX;
    public AudioClip shurikenHitSFX;
    public AudioClip shurikenHitWoodSFX;
    public AudioClip shurikenHitTrapSFX;
    public List<AudioClip> shurikenThrowSFX;
    public List<AudioClip> shurikenHitCrystalSFX;

    [Header("---------------Enemy's Audio Clips---------------")]
    public List<AudioClip> enemyAttackSFX;
    public List<AudioClip> enemyDeadSFX;
    public List<AudioClip> enemyDetectSFX;
    public List<AudioClip> enemyNotDetectSFX;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SfxSource.PlayOneShot(clip);
    }

    public void PlayListSFX(List<AudioClip> clips)
    {
        int randomIndex = Random.Range(0, clips.Count);
        SfxSource.PlayOneShot(clips[randomIndex]);
    }
}
