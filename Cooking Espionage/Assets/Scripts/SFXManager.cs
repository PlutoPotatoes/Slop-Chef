using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [SerializeField] private AudioSource SFXObject;
    [SerializeField] private AudioSource SFXLoopObject;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void playSFX(AudioClip clip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(SFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void playSFXLoop(AudioClip clip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(SFXLoopObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
    }
}
