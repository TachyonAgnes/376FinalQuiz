using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public float max;
    public static float current;
    public Image progress;
    private bool isLose;
    private float restartCounter;

    // Start is called before the first frame update
    void Start()
    {
        isLose = false;
        current = 100;
        restartCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
        if (isLose)
        {
            restartCounter += Time.deltaTime;
            if (restartCounter >= 5)
            {
                transform.GetChild(2).gameObject.SetActive(false);
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
