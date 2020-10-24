// using System;
// using Mumi;
// using UniRx.Async;
// using UnityEngine;
//
// public class TimeManager : SingletonT<TimeManager>
// {
//     [Header("Reward")]
//     [SerializeField] private int _hoursToGetReward = 24;
//     [SerializeField] private int _rewardCrystalQty = 3;
//     [SerializeField] private int _crystalsDayStart = 2;
//     [SerializeField] private int _crystalsDayBonus = 1;
//     [SerializeField] private int _maxCrystalsBonus = 5;
//     [Header("Mini Games")] 
//     [SerializeField] private int _bubblesUpdateMinutes = 20;
//     [SerializeField] private int _scarabUpdateMinutes = 20;
//     [Space] 
//     [SerializeField] private int _randomNotificationAddedHourMin = 5;
//     [SerializeField] private int _randomNotificationAddedHourMax = 50;
//     [Space]
//     [Range(1f, 10f)]
//     [SerializeField] private float _updateRateSeconds = 10f;
//     [Range(0f, 0.1f)]
//     [SerializeField] private float _regressionProgress = 0.02f;
//
//     // public int RewardCrystals => _rewardCrystalQty;
//     public int BonusCrystalsForReward { get; private set; }
//
//
//     private async void Start()
//     {
//         await UpdateLastVisitTime();
//         await UpdateButtonsProgress();
//     }
//     
//     private async void OnApplicationFocus(bool focusStatus) 
//     {
//         if (!focusStatus)
//         {
//             return;
//         }
//         
//         await UpdateLastVisitTime();
//         
//         // if (LocalSettings.IsNeedToGetReward)
//         // {
//         //     // MumiController.Instance.Reset();
//         // }
//     }
//
//     private async void OnApplicationQuit()
//     {
//         await UpdateLastVisitTime();
//     }
//
//     private async UniTask UpdateButtonsProgress()
//     {
//         while (Application.isPlaying)
//         {
//             if (LocalSettings.IsFirstGame)
//             {
//                 await UniTaskUtils.Delay(2f);
//                 continue;
//             }
//
//             CalculateButtonsProgress();
//             await UniTaskUtils.Delay(_updateRateSeconds);
//         }
//     }
//     
//     private void CalculateButtonsProgress()
//     {
//         double bubblesMinutesLeft = new TimeSpan(LocalSettings.AvailableTimeToPlayBubbles.Ticks - DateTime.UtcNow.Ticks)
//             .TotalMinutes;
//         double scarabMinutesLeft = new TimeSpan(LocalSettings.AvailableTimeToPlayScarab.Ticks - DateTime.UtcNow.Ticks)
//             .TotalMinutes;
//
//         _bubblesButton.SetProgress(
//             GetBubblesProgress((float)bubblesMinutesLeft), (int) bubblesMinutesLeft);
//         _scarabButton.SetProgress(
//             GetScarabProgress((float)scarabMinutesLeft), (int) scarabMinutesLeft);
//     }
//
//     private float GetBubblesProgress(float minutesLeft)
//     {
//         return minutesLeft / _bubblesUpdateMinutes - _regressionProgress;
//     }
//     
//     private float GetScarabProgress(float minutesLeft)
//     {
//         return minutesLeft / _scarabUpdateMinutes - _regressionProgress;
//     }
//
//     public void BubblesMiniGamePlayed()
//     {
//         LocalSettings.AvailableTimeToPlayBubbles = DateTime.UtcNow.AddMinutes(_bubblesUpdateMinutes);
//         CalculateButtonsProgress();
//     }
//     
//     public void ScarabMiniGamePlayed()
//     {
//         LocalSettings.AvailableTimeToPlayScarab = DateTime.UtcNow.AddMinutes(_scarabUpdateMinutes);
//         CalculateButtonsProgress();
//     }
//
//     private async UniTask UpdateLastVisitTime()
//     {
//         if ((DateTime.UtcNow - LocalSettings.LastVisitTime).Days > 0
//             || LocalSettings.IsFirstGame)
//         {
//             LocalSettings.AvailableTimesToWatchAdPerDay = 5;
//         }
//         
//         LocalSettings.LastVisitTime = DateTime.UtcNow;
//         if (DateTime.UtcNow < LocalSettings.AvailableTimeToGetReward
//             && !LocalSettings.IsFirstGame)
//         {
//             return;
//         }
//
//         LocalSettings.IsNeedToGetReward = !LocalSettings.IsFirstGame;
//         LocalSettings.RewardsGotQty++;
//         BonusCrystalsForReward = Mathf.Min(
//             LocalSettings.RewardsGotQty > 0 
//                                            ? LocalSettings.RewardsGotQty * _crystalsDayBonus + _crystalsDayStart
//                                            : 0, 
//             _maxCrystalsBonus);
//
//         LocalSettings.AvailableTimeToGetReward = LocalSettings.LastVisitTime.AddHours(_hoursToGetReward);
//
//         await _notificationsManager.WaitPlatformInitialization();
//         _notificationsManager.CancelNotification(Notifications.RewardValueId);
//         _notificationsManager.SendNotification(StaticNames.GameName,
//             LeanLocalization.GetTranslationText("notification_reward"), 
//             LocalSettings.AvailableTimeToGetReward,
//             Notifications.RewardValueId,
//             smartTime: true,
//             channel:NotificationChannels.Minigames, largeIcon: NotificationsIcons.LogoLarge);
//     }
//
//     public void SetNeedToGetRewardStatus(bool status)
//     {
//         LocalSettings.IsNeedToGetReward = status;
//     }
//
//     public void RewardCollected()
//     {
//         LocalSettings.IsNeedToGetReward = false;
//     }
// }