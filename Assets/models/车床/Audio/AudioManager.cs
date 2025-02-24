using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [HideInInspector]
    public AudioSource audio;

    public AudioSource scSound, coSound, miSound, sound;

    public AudioClip[] audios;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        audio = GetComponent<AudioSource>();
    }

    public void Play()
    {

        audio.Play();
    }
    public void Stop()
    {
        audio.Stop();
        //sound.Stop();

    }
    public void MiStop()
    {
        miSound.Stop();
    }
    public void PlayByOrder()
    {
        StartCoroutine(ByOrder());
    }
    IEnumerator ByOrder()
    {
        coSound.Play();
        yield return new WaitForSeconds(1);
        scSound.Play();
        yield return new WaitForSeconds(1);
        sound.Play();
    }
    IEnumerator ByOrder2()
    {
        scSound.Play();
        yield return new WaitForSeconds(1.5f);
        sound.Play();

    }
    public void PlayByOrder2()
    {
        sound.clip = audios[0];
        StartCoroutine(ByOrder());
    }

    public void PlayByOrder3()
    {
        sound.clip = audios[1];
        StartCoroutine(ByOrder());
    }
    public void PlayByOrder4()
    {
        sound.clip = audios[2];
        StartCoroutine(ByOrder2());
    }
    public void PlayByOrder5()
    {
        sound.clip = audios[14];
        StartCoroutine(ByOrder2());
    }
    public void PlayByOrder6()
    {
        sound.clip = audios[12];
        StartCoroutine(ByOrder2());
    }
    public void PlayByOrder7()
    {
        sound.clip = audios[13];
        StartCoroutine(ByOrder2());
    }
    public void PlayMiching()
    {
        miSound.Play();
    }
    ///// <summary>
    ///// 注意对象的ToString方法和name方法的区别
    ///// </summary>
    ///// <param name="s"></param>
    //public void ChoseClip(string s)
    //{
    //    for (int i = 0; i < audios.Length; i++)
    //    {
    //        if (string.Equals(audios[i].name, s))
    //        {
    //            audio.clip = audios[i];
    //            //Debug.Log(2);
    //        }
    //    }
    //}

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
