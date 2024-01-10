using System;
using UnityEngine;

public static class SystemManager 
{
    public static void SystemReset()
    {
        Debug.Log("System reset..");
        //Set screen to default value: black
        ScreenColor.instance.SetColorBlack();
        //Remove all labels
        if (LabelManager.instance.GetLabelCount() != 0)
        {
            for(int i = 0; i < LabelManager.instance.GetLabelCount(); i++)
            {
                GameObject label = GameObject.Find("Label" + i);
                LabelManager.instance.RemoveLabel(label);
            }
        }
        //Turn of the led
        LedsFunctionality.instance.LedOff();
        //Reset speaker
        SpeakerManager.instance.SetVolume(0.5f);
        SpeakerManager.instance.ResetAudioClips();
        //Set Date to Now
        RTCFunctions.SetDateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                 DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        //Stop WDT
        if (TimerFunctions.instance.IsActive())
        {
            TimerFunctions.instance.StopTimer();
        }
    }
}
