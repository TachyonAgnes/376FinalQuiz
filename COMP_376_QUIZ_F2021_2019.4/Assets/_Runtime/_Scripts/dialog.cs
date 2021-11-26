using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ------------------------------------------------------------------------------ 
// Quiz 
// Written by: Zisen Ling & 40020293
// For COMP 376 – Fall 2021 
// ----------------------------------------------------------------------------- 
public class dialog : MonoBehaviour
{
    public static bool isTalkDone;
    private bool isWin;
    private bool isLose;
    private bool isGameStart;
    private float gameStartCounter;

    // Start is called before the first frame update
    void Start()
    {
        isTalkDone = false;
        isWin = HealthBar.isWin;
        isLose = HealthBar.isLose;
        isGameStart = false;
        gameStartCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown("y"))
        {
            isTalkDone = true;
        }
        if (isTalkDone)
        {
            if (isGameStart)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false);
                gameStartCounter += Time.deltaTime;
                if (gameStartCounter >= 5)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(false);
                }
            }
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
            isGameStart = true;
        }

        if (isWin)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(3).gameObject.SetActive(false);
        }
        if (isLose)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(true);
        }


    }
}
