using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IMediationManager
{
    void Init();

    void LoadOpenAd();
    void ShowOpenAd();
    void LoadInterstitialAd();
    void ShowInterstitialAd(Action _complete, Action _fail, string _placement);

    void LoadRewardedAd();
    void ShowRewardedAd(Action _complete, Action _fail, string _placement);

    void LoadBanner();
    void HideBanner();
    void ShowBanner();

}
