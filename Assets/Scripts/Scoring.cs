using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public static Scoring m_instance;
    private bool winState;

    public static Scoring instance
    {
        get
        {
            if(m_instance == null) m_instance = new Scoring();
            return m_instance;
        }
    }

    void Awake()
    {
        m_instance = this;
    }

    public void Win()
    {
        DroneSpawner.instance.Restart();
        if(!winState)
        {
            Debug.Log("Activate goal sequence!");
            winState = true;
        }
    }
}
