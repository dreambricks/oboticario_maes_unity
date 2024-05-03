using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitPrint : MonoBehaviour
{
    [SerializeField] private BuildYourLetter buildYourLetter;

    public float totalTime;
    private float currentTime;


    private void Update()
    {
        Countdown();
    }

    private void OnEnable()
    {
        currentTime = totalTime;
    }
    public void Countdown()
    {
        currentTime -= Time.deltaTime;


        if (currentTime <= 0)
        {
            currentTime = 0;

            //DataLog dataLog = new();
            //dataLog.status = StatusEnum.NaoJogou.ToString();
            //LogUtil.SendLogCSV(dataLog);

            buildYourLetter.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
