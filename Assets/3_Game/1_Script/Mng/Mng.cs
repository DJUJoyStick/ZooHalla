using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mng : MonoBehaviour
{
    private static Mng _Instance = null;
    public AudioSource MainAudio = null;
    [SerializeField]
    AudioSource EffectAudio = null;
    [SerializeField]
    AudioSource EffectAudio2 = null;
    [SerializeField]
    AudioClip[] SoundClip = null;


    public static Mng I
    {
        get
        {
            if (_Instance.Equals(null))
            {
                Debug.Log("Instance is null");
            }
            return _Instance;
        }
    }

    public Char CharSc;

    public Sprite[] PortraitSp = new Sprite[3];

    public UnityEngine.UI.Image PortraitImg;

    public PLAYERTYPE SelectedType;

    public int nUnimplemented = 0;

    public bool bSelected = false;

    public bool bBackGroundSound;       // true ¹è°æÀ½¾Ç ³ª¿È false ¾È³ª¿È
    public bool bEffectSound;           // true È¿°úÀ½ ³ª¿È false ¾È³ª¿È


    private void Awake()
    {
        _Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Play(string sSoundName, bool bEffectAudio, bool bAutoPlay)
    {
        if (bAutoPlay.Equals(true))
        {
            MainAudio.playOnAwake = true;
            MainAudio.loop = true;
        }
        else
        {
            MainAudio.playOnAwake = false;
            MainAudio.loop = false;
        }

        for (int i = 0; i < SoundClip.Length; i++)
        {
            if (!bEffectAudio && SoundClip[i].name.Equals(sSoundName))
            {
                MainAudio.clip = SoundClip[i];
                MainAudio.Play();
            }
            else if (!EffectAudio.isPlaying && bEffectAudio && SoundClip[i].name.Equals(sSoundName))
            {
                EffectAudio.clip = SoundClip[i];
                EffectAudio.Play();
            }
            else if (EffectAudio.isPlaying && bEffectAudio && SoundClip[i].name.Equals(sSoundName))
            {
                EffectAudio2.clip = SoundClip[i];
                EffectAudio2.Play();
            }
        }
    }

    void SoundOff()
    {
        if (bBackGroundSound)
        {
            if (!MainAudio.playOnAwake || !MainAudio.loop)
            {
                MainAudio.playOnAwake = true;
                MainAudio.loop = true;
            }
            MainAudio.enabled = true;
        }
        else
        {
            MainAudio.enabled = false;
        }

        if (bEffectSound)
        {
            EffectAudio.enabled = true;
            EffectAudio2.enabled = true;
        }
        else
        {
            EffectAudio.enabled = false;
            EffectAudio2.enabled = false;
        }
    }

    IEnumerator SoundCtrl()
    {
        yield return null;
        SoundOff();

        StartCoroutine("SoundCtrl");
    }

}
