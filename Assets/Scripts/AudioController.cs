using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController m_instance;
    public static AudioController instance
    {
        get
        {
            if(m_instance == null) m_instance = new AudioController();
            return m_instance;
        }
    }
    void Awake()
    {
        m_instance = this;
    }

    public AudioSource jamAudio;
    public AudioSource fallAudio;
    public AudioSource beepAudio;
    public AudioSource copterAudio;
    public int isJammed = 0;
    public bool isMoving = false;
    void Update()
    {
        if(isJammed > 0)
        {
            if(jamAudio.volume < 0.5)
            {
                jamAudio.volume += 0.001f;
            }
        }
        else
        {
            if(jamAudio.volume > 0.0)
            {
                jamAudio.volume -= 0.02f;
            }

        }
        
        if(isMoving)
        {
            if(copterAudio.volume < 0.2)
            {
                copterAudio.volume += 0.007f;
            }
        }
        else
        {
            if(copterAudio.volume > 0.0)
            {
                copterAudio.volume -= 0.007f;
            }

        }
    }

    public void Fall()
    {
        fallAudio.Play();
    }
    
    public void JustBeep()
    {
        beepAudio.Play();
    }
}
