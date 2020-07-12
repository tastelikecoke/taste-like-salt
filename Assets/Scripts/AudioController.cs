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
    public AudioSource musicAudio;
    public int isJammed = 0;
    public bool isMoving = false;
    public bool isMovingFast = false;
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
        
        if(isMovingFast && isMoving)
        {
            if(copterAudio.volume < 0.8)
            {
                copterAudio.volume += 0.009f;
            }
        }
        else if(isMoving)
        {
            if(copterAudio.volume < 0.4)
            {
                copterAudio.volume += 0.007f;
            }
            if(copterAudio.volume > 0.4)
            {
                copterAudio.volume -= 0.007f;
            }
        }
        else
        {
            if(copterAudio.volume > 0.0)
            {
                copterAudio.volume -= 0.007f;
            }

        }

        
        if(Input.GetKeyDown(KeyCode.M))
        {
            if(musicAudio.volume == 1.0f)
                musicAudio.volume = 0.0f;
            else
                musicAudio.volume = 1.0f;

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
