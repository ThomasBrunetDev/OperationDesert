using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip _ambiance;

    private AudioSource _audio;
    private static SoundManager _instance;

    public static SoundManager instance
    {
        get { return _instance; }
    }

    // Singleton
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        if (_audio == null)
        {
            _audio = (AudioSource)gameObject.AddComponent<AudioSource>();
        }
        _audio.loop = true;
        JouerAmbiance(_ambiance);
        if (_ambiance != null) _audio.Play();
    }

    public void JouerSon(AudioClip clip)
    {
        _audio.PlayOneShot(clip);
    }

    public void JouerAmbiance(AudioClip clip, bool isLooping = true)
    {
        _audio.clip = clip;
        _audio.loop = isLooping;
    }
}