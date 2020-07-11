using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public static Scoring m_instance;
    private bool winState;
    private Level currentLevel;
    public List<Level> levelList;
    private int levelIndex;

    public Level CurrentLevel
    {
        get { return currentLevel; }
    }

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

    void Start()
    {
        currentLevel = levelList[0];
    }

    public void Win()
    {
        if(levelIndex >= levelList.Count) return;
        currentLevel.spawner.Die();
        currentLevel.gameObject.SetActive(false);

        levelIndex += 1;
        currentLevel = levelList[levelIndex];
        currentLevel.gameObject.SetActive(true);
        currentLevel.spawner.Restart();
    }
}
