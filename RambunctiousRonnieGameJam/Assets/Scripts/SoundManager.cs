using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Trait;

public class SoundManager : MonoBehaviour
{
    EventCore eventCore;

    public AudioSource audioSourceBGM;
    public AudioSource audioSourceSFX;
    [Header("Audio")]
    public LoopingBgm currentBgm;
    public float originalVolume;
    public List<AudioClip> winLoseMusic;
    public List<LoopingBgm> genreMusic;
    public List<AudioClip> variousSfx;

    [Header("Miscellanous")]
    public Settings settings;

    bool gameDone;

    private void Awake()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.updateGenreEV.AddListener(ChangeMusic);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();

        eventCore.updateGenreEV.AddListener(ChangeMusic);
        eventCore.playOneShotEV.AddListener(PlayOneShot);
        eventCore.playVariousSfxEV.AddListener(PlayVariousSfx);

        eventCore.winGameEV.AddListener(PlayWinMusic);
        eventCore.loseGameEV.AddListener(PlayLoseMusic);
    }

    // Update is called once per frame
    void Update()
    {
        audioSourceBGM.volume = originalVolume * settings.musicVolume;
        audioSourceSFX.volume = settings.sfxVolume;
        if (!gameDone)
            PlayLoopingBgm();
    }

    public void PlayOneShot(AudioClip sfx)
    {
        audioSourceSFX.PlayOneShot(sfx);
    }

    void PlayVariousSfx(int sfx)
    {
        audioSourceSFX.PlayOneShot(variousSfx[sfx]);
    }

    //changes the music to be the corresponding genre
    void ChangeMusic(Genres genre)
    {
        print("changing music");
        currentBgm = genreMusic[(int)genre];
        originalVolume = currentBgm.volume;
        print($"current bgm: {currentBgm}");
        StartCoroutine(TransitionToNextBGM());
    }

    IEnumerator TransitionToNextBGM()
    {
        audioSourceBGM.clip = variousSfx[7]; //play static sfx
        audioSourceBGM.loop = true;
        audioSourceBGM.Play();
        
        yield return new WaitForSeconds(2);

        audioSourceBGM.loop = false;
        audioSourceBGM.clip = currentBgm.bgm;
        audioSourceBGM.Play();
    }

    void PlayLoopingBgm()
    {
        if (currentBgm == null) return;
        
        if (currentBgm.loopEnd >= 0)
        {
            int loopEnd = (int) (currentBgm.loopEnd * currentBgm.bgm.frequency); 
            int loopStart = (int) (currentBgm.loopStart * currentBgm.bgm.frequency);
            int loopLength = loopEnd - loopStart;
            if (audioSourceBGM.timeSamples >= loopEnd)
            {
                audioSourceBGM.timeSamples -= loopLength;
            }
        }
        else
        {
            //int loopEnd = (int)currentBgm.bgm.samples;
            
            int loopStart = (int)currentBgm.loopStart * currentBgm.bgm.frequency;
            if (!audioSourceBGM.isPlaying)
            {
                audioSourceBGM.Play();
                audioSourceBGM.timeSamples = loopStart;
            }
        }
    }

    void PlayWinMusic()
    {
        gameDone = true;
        audioSourceBGM.loop = false;
        audioSourceBGM.clip = winLoseMusic[0];
        audioSourceBGM.Play();
    }

    void PlayLoseMusic()
    {
        gameDone = true;
        audioSourceBGM.loop = false;
        audioSourceBGM.clip = winLoseMusic[1];
        audioSourceBGM.Play();
    }

}
