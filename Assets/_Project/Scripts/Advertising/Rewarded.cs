using _Project.Scripts.GameFlow;
using UnityEngine;
using UnityEngine.Advertisements;

namespace _Project.Scripts.Advertising
{
    [RequireComponent(typeof(GameOver))]
    public class Rewarded : MonoBehaviour, IRewarded, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] private string _androidAdUnitId = "Rewarded_Android";
        [SerializeField] private string _iOSAdUnitId = "Rewarded_iOS";

        private string _adUnitId = null;
        private GameOver _gameOver;

        private void Awake()
        {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
            _adUnitId = _androidAdUnitId;
#endif

            _gameOver = GetComponent<GameOver>();
        }

        public void ShowAd()
        {
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
                _gameOver.ContinueGame();
            }
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowStart(string adUnitId) { }
        public void OnUnityAdsShowClick(string adUnitId) { }
    }
}