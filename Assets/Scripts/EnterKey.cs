﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterKey : MonoBehaviour
{
    private int state;
    public Text enterText;
    public Text firstText;
    public Text secondText;
    public Text thirdText;
    public GameObject credits;
    void Awake()
    {
        state = 0;
        firstText.gameObject.SetActive(true);
        StartCoroutine(WaitForEnter());
    }
    void FixedUpdate()
    {

        if(state == 3)
        {
            credits.transform.position += new Vector3(0.0f, 0.03f, 0.0f);
        }
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(!enterText.gameObject.activeSelf) return;
            if(state == 0)
            {
                firstText.gameObject.SetActive(false);
                secondText.gameObject.SetActive(true);
                
                int finalDroneCount = PlayerPrefs.GetInt("drones", 1);
                if(finalDroneCount == 1)
                {
                    secondText.text = string.Format("and it only took us {0} drone! Amazing!", finalDroneCount);
                }
                else
                    secondText.text = string.Format("and it only took us {0} drones!", finalDroneCount);
                StartCoroutine(WaitForEnter());
                state = 1;
            }
            else if(state == 1)
            {
                secondText.gameObject.SetActive(false);
                thirdText.gameObject.SetActive(true);

                StartCoroutine(WaitForEnter());
                state = 2;
            }
            else if(state == 2)
            {
                thirdText.gameObject.SetActive(false);
                credits.gameObject.SetActive(true);
                StartCoroutine(WaitForEnterLong());
                
                state = 3;
            }
            else if (state == 3)
            {
                SceneManager.LoadScene("Title");
            }
        }
    }

    IEnumerator WaitForEnter()
    {
        enterText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.50f);
        enterText.gameObject.SetActive(true);
        yield return null;
    }
    IEnumerator WaitForEnterLong()
    {
        enterText.gameObject.SetActive(false);
        yield return new WaitForSeconds(8.0f);
        enterText.gameObject.SetActive(true);
        yield return null;
    }
}
