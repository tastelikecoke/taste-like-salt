using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCounter : MonoBehaviour
{
    public Text levelText;
    void Update()
    {
        levelText.text = string.Format("Level {0}", Scoring.instance.levelIndex + 1);

        if(Scoring.instance.levelIndex + 1 == 12)
        {
            levelText.text = "Final level";
        }
    }
}
