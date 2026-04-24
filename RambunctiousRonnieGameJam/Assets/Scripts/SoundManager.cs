using System.Collections.Generic;
using UnityEngine;
using static Trait;

public class SoundManager : MonoBehaviour
{
    EventCore eventCore;

    AudioSource audioSource;
    public List<AudioClip> genreMusic;
    public List<AudioClip> variousSfx;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.updateGenreEV.AddListener(ChangeMusic);
        eventCore.playOneShotEV.AddListener(PlayOneShot);
        eventCore.playVariousSfxEV.AddListener(PlayVariousSfx);

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayOneShot(AudioClip sfx)
    {
        audioSource.PlayOneShot(sfx);
    }

    void PlayVariousSfx(int sfx)
    {
        audioSource.PlayOneShot(variousSfx[sfx]);
    }

    //changes the music to be the corresponding genre
    void ChangeMusic(Genres genre)
    {

    }
}
