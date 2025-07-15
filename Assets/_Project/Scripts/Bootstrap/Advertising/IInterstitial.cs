using System;

namespace _Project.Scripts.Bootstrap.Advertising
{
    public interface IInterstitial
    {
        public void ShowAd(Action onAdShown);
    }
}