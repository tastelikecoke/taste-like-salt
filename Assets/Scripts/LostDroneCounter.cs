using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LostDroneCounter : MonoBehaviour
{
    public Text lostDroneText;
    void Update()
    {
        if(Scoring.instance.CurrentLevel.spawner.lostDrones == 0)
            lostDroneText.text = "";
        else if(Scoring.instance.CurrentLevel.spawner.lostDrones == 1)
            lostDroneText.text = "1 lost drone";
        else
            lostDroneText.text = string.Format("{0} lost drones", Scoring.instance.CurrentLevel.spawner.lostDrones);
    }
}
