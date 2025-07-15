using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace _Project.Scripts.Bootstrap.Advertising
{
    public class Rewarded : IRewarded, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private string _androidAdUnitId = "Rewarded_Android";
        private string _iOSAdUnitId = "Rewarded_iOS";

        private string _adUnitId;

        private Action OnAdShown;

        public Rewarded()
        {
            _adUnitId = Application.platform == RuntimePlatform.IPhonePlayer
                ? _iOSAdUnitId
                : _androidAdUnitId;
        }

        public void ShowAd(Action onAdShown)
        {
            OnAdShown = onAdShown;
            Advertisement.Show(_adUnitId, this);
        }

        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log("Ad Loaded: " + adUnitId);
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                Debug.Log("Unity Ads Rewarded Ad Completed");
                OnAdShown?.Invoke();
                OnAdShown = null;
            }
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.LogError($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.LogError($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
            Debug.Log("Rewarded Ad Show Started");
        }
        public void OnUnityAdsShowClick(string adUnitId)
        {
            Debug.Log("Rewarded Ad Clicked");
        }
    }
}