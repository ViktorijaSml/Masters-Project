using System.Collections;
using UnityEngine;

public class TimerFunctions : MonoBehaviour
{
    private bool isActiveFeedWDT = false;
   
    void Start()
    {
        StartCoroutine(StartCountdown(5000));
        StartCoroutine(Wait(2));
        FeedWDT();
    }

    public void FeedWDT()
    {
        isActiveFeedWDT = true;
    }
    public IEnumerator StartCountdown(int miliseconds)
    {
        for (int i = miliseconds/1000; i > 0; i--)
        {
            if (isActiveFeedWDT)
            {
                isActiveFeedWDT = false;
                i = miliseconds/1000;
                Debug.Log("Reseting timer... " + i);
            }
            else
            {
                Debug.Log(i);
                yield return new WaitForSeconds(1);
            }
        }
        throw new System.Exception("WDT has finished! There must be an error!");
    }
    public IEnumerator Wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("Im waiting!");
    }
}
