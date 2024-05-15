using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SystemManager 
{
    public static void RefreshApp()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public static void SystemReset()
    {
        ResetLabels();
        ResetScreen();
        ResetLed();
        ResetSpeaker();
        ResetDate();
        ResetWDT();
    }
    public static void ResetScreen() => ScreenManager.instance.SetColorBlack();
    public static void ResetLed() => LedManager.instance.LedOff();
    public static void ResetSpeaker()
    {
        SpeakerManager.instance.SetVolume(0.5f);
        SpeakerManager.instance.ResetAudioClips();
    }
    public static void ResetDate() => RTCFunctions.SetDateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                 DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
    public static void ResetWDT()
    {
        if (TimerFunctions.instance.IsActive())
        {
            TimerFunctions.instance.StopTimer();
        }
    }
    public static void ResetLabels()
    {
        GameObject displayText = LabelManager.instance.GetDisplayTextObject();

        if (LabelManager.instance.GetLabelCount() != 0)
        {
            foreach (Transform label in displayText.transform)
            {
                LabelManager.instance.RemoveLabel(label.gameObject);
            }
        }
    }

}
