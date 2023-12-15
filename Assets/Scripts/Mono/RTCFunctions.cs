using System;
using UnityEngine;
public static class RTCFunctions 
{
    private static DateTime dateTime = DateTime.Now;
    public static string WriteDate (DateTime date) 
    {
        string dateText = dateTime.ToString("dd.MM.yyyy HH:mm");
        Debug.Log(dateText);
        return dateText;
    }
    public static void SetDateTime(int year, int month, int day, int hour, int minute, int second )
    {
        dateTime = new DateTime(year, month, day, hour, minute, second);
        WriteDate(dateTime);
    }
    public static int[] GetLocalTime()
    {
        //pisem bas kako su u M5Flow stranici to izrealizirali
        var localDate = DateTime.Now;
        int[] localDateInt = new int[6];

        localDateInt[0] = localDate.Year;
        localDateInt[1] = localDate.Month;
        localDateInt[2] = localDate.Day;
        localDateInt[3] = localDate.Hour;
        localDateInt[4] = localDate.Minute;
        localDateInt[5] = localDate.Second;

        return localDateInt;
    }
    public static int GetSecond() => dateTime.Second;
    public static int GetMinute() => dateTime.Minute;
    public static int GetHour() => dateTime.Hour;
    public static int GetDay() => dateTime.Day;
    public static int GetMonth() => dateTime.Month;
    public static int GetYear() => dateTime.Year;

}
