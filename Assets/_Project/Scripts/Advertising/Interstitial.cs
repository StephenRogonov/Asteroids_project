using _Project.Scripts.GameFlow;
using UnityEngine;
using UnityEngine.Advertisements;

namespace _Project.Scripts.Advertising
{
    [RequireComponent(typeof(GameOver))]
    public class Interstitial : MonoBehaviour, IInterstitial, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] private string _androidAdUnitId = "Interstitial_Android";
        [SerializeField] private string _iOsAdUnitId = "Interstitial_iOS";

        private string _adUnitId;
        private GameOver _gameOver;

        private void Awake()
        {
            _adUnitId = Application.platform == RuntimePlatform.IPhonePlayer
                ? _iOsAdUnitId
                : _androidAdUnitId;

            _gameOver = GetComponent<GameOver>();
        }

        public void ShowAd()
        {
            Debug.Log("Showing Ad: " + _adUnitId);
            Advertisement.Show(_adUnitId, this);
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            _gameOver.RestartGame();
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsAdLoaded(string adUnitId) { }

        public void OnUnityAdsShowStart(string adUnitId) { }

        public void OnUnityAdsShowClick(string adUnitId) { }
    }
}