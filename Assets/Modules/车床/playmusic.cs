using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playmusic : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] public  AudioClip[] audioClips;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[0];
    }
    void Update()
    {
        
    }
    public void Playqiege()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }
    
    public void Playxuanzhuan()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }
    
    public void Playjinggao()
    {
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }
    public void Pause()
    {
        audioSource.Pause();
    }
}
