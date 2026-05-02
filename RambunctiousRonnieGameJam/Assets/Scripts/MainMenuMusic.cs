using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    AudioSource audioSource;
    public Settings settings;
    
    public AudioClip staticSound;
    public LoopingBgm[] genreBgm;
    public float minDelay = 2;
    public float maxDelay = 4;
    public float minTimestamp = 6;
    public float maxTimestamp = 20; //make sure the max timestamp is not too high when added to max delay, where it reaches the end of the song
    public float minStaticLength = 0.25f;
    public float maxStaticLength = 1.5f;
    float originalVolume;

    float delay;
    float timer;

    bool transitioning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = originalVolume * settings.musicVolume;

        timer += Time.deltaTime;
        
        if ((timer > delay && !transitioning) || audioSource.clip == null)
        {
            StartCoroutine(TransitionToNextBGM());
        }
    }

    IEnumerator TransitionToNextBGM()
    {
        transitioning = true;

        audioSource.clip = staticSound;
        audioSource.loop = true;
        audioSource.Play();

        float randomLength = Random.Range(minStaticLength, maxStaticLength);
        print(randomLength);

        yield return new WaitForSeconds(randomLength);

        RandomizeBgm();
    }

    void RandomizeBgm()
    {
        timer = 0;

        int randomNum = Random.Range(0, genreBgm.Length);
        float randomTimestamp = Random.Range(minTimestamp, maxTimestamp);
        delay = Random.Range(minDelay, maxDelay);
        
        LoopingBgm currentBgm = genreBgm[randomNum];
        originalVolume = currentBgm.volume;

        audioSource.loop = false;
        audioSource.clip = currentBgm.bgm;
        audioSource.Play();

        audioSource.timeSamples = (int) (randomTimestamp * currentBgm.bgm.frequency);
        transitioning = false;
    }
}
