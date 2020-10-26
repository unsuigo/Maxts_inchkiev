using System;
using UnityEngine;

public static class LocalSettings
{
    private const string StarsGotQtyKey = "StarsGotQty";
    private const string IsGameCompletedKey = "IsGameCompleted";
    private const string AvailableTimeToPlayKey = "AvailableTimeToPlay";
    private const string IsMusicEnabledKey = "IsMusicEnabled";
    private const string IsSoundsEnabledKey = "IsSoundsEnabled";
    private const string VersionKey = "Version";
    
  
    public static int StarsGotQty
    {
        get => PlayerPrefs.GetInt(StarsGotQtyKey, -2);
        set => PlayerPrefs.SetInt(StarsGotQtyKey, value);
    }

    public static bool IsGameCompleted
    {
        get => PrefsUtils.GetBool(IsGameCompletedKey, false);
        set => PrefsUtils.SetBool(IsGameCompletedKey, value);
    }
    
    public static DateTime AvailableTimeToPlay
    {
        get => PrefsUtils.GetDate(AvailableTimeToPlayKey, DateTime.MinValue);
        set => PrefsUtils.SetDate(AvailableTimeToPlayKey, value);
    }
   
    
    //Sound
    public static bool IsMusicEnabled
    {
        get => PrefsUtils.GetBool(IsMusicEnabledKey, true);
        set => PrefsUtils.SetBool(IsMusicEnabledKey, value);
    }
    
    public static bool IsSoundsEnabled
    {
        get => PrefsUtils.GetBool(IsSoundsEnabledKey, true);
        set => PrefsUtils.SetBool(IsSoundsEnabledKey, value);
    }
  
    public static float CurrentVersion
    {
        get => PlayerPrefs.GetFloat(VersionKey, 1.0f);
        set => PlayerPrefs.SetFloat(VersionKey, value);
    }
}
