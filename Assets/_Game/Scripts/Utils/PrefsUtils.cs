using System;
using UnityEngine;

public static class PrefsUtils
{
    public static bool GetBool(string key, bool defaultValue)
    {
        return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
    }

    public static void SetBool (string key, bool state)
    {
        PlayerPrefs.SetInt(key, state ? 1 : 0);
    }

    public static DateTime GetDate(string key, DateTime defaultValue)
    {
        return DateTime.TryParse(PlayerPrefs.GetString(key, defaultValue.ToString()), 
            out DateTime time)
            ? time 
            : DateTime.UtcNow;
    }

    public static void SetDate(string key, DateTime dateTime)
    {
        PlayerPrefs.SetString(key, dateTime.ToString());
    } 
    
    public static void GetInt(string key, int i)
    {
        PlayerPrefs.GetInt(key, i);
    } 
    
    public static void SetInt(string key, int i)
    {
        PlayerPrefs.SetInt(key, i);
    } 
}