using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// ------------------------------------------------------------------------------ 
// Quiz 
// Written by: Zisen Ling & 40020293
// For COMP 376 – Fall 2021 
// ----------------------------------------------------------------------------- 

public class HealthBar : MonoBehaviour
{
    public float max;
    public static float current;
    public Image progress;
    public static bool isLose;
    public static bool isWin;
    private float restartCounter;

    // Start is called before the first frame update
    void Start()
    {
        isLose = false;
        isWin = false;
        current = 100;
        restartCounter = 0;
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
        if (Input.GetKeyDown("n"))
        {
            transform.GetChild(4).gameObject.SetActive(false);
            transform.GetChild(5).gameObject.SetActive(true);
        }
        if (dialog.isTalkDone)
        {
            transform.GetChild(4).gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            transform.GetChild(4).gameObject.SetActive(true);
            transform.GetChild(5).gameObject.SetActive(false);
        }
        if (isLose)
        {
            restartCounter += Time.deltaTime;
            if (restartCounter >= 5)
            {
                SceneManager.LoadScene("BombombBattlefield");
            }
        }
        if (isWin)
        {
            transform.GetChild(3).gameObject.SetActive(true);
            restartCounter += Time.deltaTime;
            if (restartCounter >= 5)
            {
                SceneManager.LoadScene("BombombBattlefield");
            }
        }
        if (current == 0)
        {
            transform.GetChild(2).gameObject.SetActive(true);
            isLose = true;
        }
    }

    void GetCurrentFill()
    {
        float fillAmount = current / max;
        progress.fillAmount = fillAmount;
    }
}
