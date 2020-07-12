using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scoring : MonoBehaviour
{
    public static Scoring m_instance;
    private Level currentLevel;
    public List<Level> levelList;
    [HideInInspector]
    public int levelIndex;

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
        levelIndex = 0;
        m_instance = this;
    }

    void Start()
    {
        foreach(Level level in levelList)
        {
            level.gameObject.SetActive(false);
        }
        currentLevel = levelList[0];
        currentLevel.gameObject.SetActive(true);
        currentLevel.spawner.Initialize();
    }

    public void Win()
    {
        levelIndex += 1;
        if(levelIndex >= levelList.Count)
        {
            SceneManager.LoadScene("EndScreen");
            return;
        }
        currentLevel.spawner.Die();
        currentLevel.gameObject.SetActive(false);

        currentLevel = levelList[levelIndex];
        currentLevel.gameObject.SetActive(true);
        currentLevel.spawner.Initialize();
    }
}
