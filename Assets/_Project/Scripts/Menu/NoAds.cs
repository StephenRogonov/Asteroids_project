using _Project.Scripts.Menu;
using UnityEngine;
using UnityEngine.UI;

public class NoAds : MonoBehaviour
{
    [SerializeField] private Button _noAdsButton;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.NO_ADS_PURCHASED_KEY) == false)
        {
            PlayerPrefs.SetString(PlayerPrefsKeys.NO_ADS_PURCHASED_KEY, "no");
        }

        string IsPurchased = PlayerPrefs.GetString(PlayerPrefsKeys.NO_ADS_PURCHASED_KEY);

        if (IsPurchased == "yes")
        {
            _noAdsButton.gameObject.SetActive(false);
        }
    }

    public void PurchaseNoAds()
    {
        PlayerPrefs.SetString(PlayerPrefsKeys.NO_ADS_PURCHASED_KEY, "yes");
        _noAdsButton.gameObject.SetActive(false);
    }
}
