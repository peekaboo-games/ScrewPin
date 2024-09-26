
using GoogleMobileAds.Api;
using GoogleMobileAds.Ump.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdmobManager : MonoBehaviour
{
    public static AdmobManager instance;

     public bool adTest;
     [Header("BannerAds")]
     public string Banner_ID = "";
     public string Banner_ID_IOS = "";
     [Header("InterAds")]
     public string Inter_ID = "";
     public string Inter_ID_IOS = "";
     [Header("RewardAds")]
     public string Reward_ID = "";
     public string Reward_ID_IOS = "";
     [Header("OpenAds")]
     public string Open_ID = "";
     public string Open_ID_IOS = "";



     private InterstitialAd interAd;
     private BannerView bannerAd;
     private AppOpenAd openAd;
     private RewardedAd rewardAd;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void Init()
     {
          MobileAds.Initialize(initStatus =>
          {
#if UNITY_IOS
Banner_ID = Banner_ID_IOS;
Inter_ID = Inter_ID_IOS;
Reward_ID = Reward_ID_IOS;
Open_ID = Open_ID_IOS;
#endif
               LoadOpenAd();
               LoadInterstitialAd();
               LoadRewardedAd();
          });

     }


     #region Open
     DateTime _expireTime;
     bool isFirstShow = false;
     private bool IsAdAvailable
     {
          get
          {
               return openAd != null
                  && openAd.CanShowAd()
                  && DateTime.Now < _expireTime;
          }
     }
     public void LoadOpenAd()
     {
          // Clean up the old ad before loading a new one.
          if (openAd != null)
          {
               openAd.Destroy();
               openAd = null;
          }

          Debug.Log("Loading the app open ad.");

          // Create our request used to load the ad.
          var adRequest = new AdRequest.Builder().Build();

          if (adTest) Open_ID = "ca-app-pub-3940256099942544/9257395921";
          // send the request to load the ad.
          AppOpenAd.Load(Open_ID, ScreenOrientation.Portrait, adRequest,
              (AppOpenAd ad, LoadAdError error) =>
              {
                   // if error is not null, the load request failed.
                   if (error != null || ad == null)
                   {
                        Debug.LogError("app open ad failed to load an ad " +
                                     "with error : " + error);
                        return;
                   }

                   Debug.Log("App open ad loaded with response : "
                           + ad.GetResponseInfo());
                   _expireTime = DateTime.Now + TimeSpan.FromHours(4);
                   openAd = ad;
                   RegisterEventHandlers(ad);
              });
     }
     private void RegisterEventHandlers(AppOpenAd ad)
     {
          // Raised when the ad is estimated to have earned money.
          ad.OnAdPaid += (AdValue adValue) =>
          {
               Debug.Log(String.Format("App open ad paid {0} {1}.",
                 adValue.Value,
                 adValue.CurrencyCode));
          };
          // Raised when an impression is recorded for an ad.
          ad.OnAdImpressionRecorded += () =>
          {
               Debug.Log("App open ad recorded an impression.");
          };
          // Raised when a click is recorded for an ad.
          ad.OnAdClicked += () =>
          {
               Debug.Log("App open ad was clicked.");
          };
          // Raised when an ad opened full screen content.
          ad.OnAdFullScreenContentOpened += () =>
          {
               Debug.Log("App open ad full screen content opened.");
          };
          // Raised when the ad closed full screen content.
          ad.OnAdFullScreenContentClosed += () =>
          {
               Debug.Log("App open ad full screen content closed.");
               LoadOpenAd();
          };
          // Raised when the ad failed to open full screen content.
          ad.OnAdFullScreenContentFailed += (AdError error) =>
          {
               Debug.LogError("App open ad failed to open full screen content " +
                            "with error : " + error);
               LoadOpenAd();
          };
     }
     public void ShowOpenAd()
     {
          if (openAd != null && openAd.CanShowAd())
          {
               if (!isFirstShow)
               {
                    isFirstShow = true;
               }
               else
               {
                    if (!IsAdAvailable) return;
               }
               openAd.Show();
          }
          else
          {

          }
     }
     #endregion
     #region Banner
     public void LoadBanner()
     {
          // create an instance of a banner view first.
          if (bannerAd == null)
          {
               ShowBanner();
          }
          // create our request used to load the ad.
          var adRequest = new AdRequest.Builder()
              .AddKeyword("unity-admob-sample")
              .Build();

          // send the request to load the ad.
          Debug.Log("Loading banner ad.");
          bannerAd.LoadAd(adRequest);
     }
     public void ShowBanner()
     {
          if (bannerAd != null)
          {
               HideBanner();
          }
          AdSize adSize = AdSize.GetPortraitAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
          if (adTest) Banner_ID = "ca-app-pub-3940256099942544/6300978111";
          bannerAd = new BannerView(Banner_ID, adSize, AdPosition.Bottom);
          ListenToAdEvents();
     }
     public void HideBanner()
     {
          if (bannerAd != null)
          {
               Debug.Log("Destroying banner ad.");
               bannerAd.Destroy();
               bannerAd = null;
          }
     }
     private void ListenToAdEvents()
     {
          // Raised when an ad is loaded into the banner view.
          bannerAd.OnBannerAdLoaded += () =>
          {
               Debug.Log("Banner view loaded an ad with response : "
                 + bannerAd.GetResponseInfo());
          };
          // Raised when an ad fails to load into the banner view.
          bannerAd.OnBannerAdLoadFailed += (LoadAdError error) =>
          {
               Debug.LogError("Banner view failed to load an ad with error : "
                 + error);
          };
          // Raised when the ad is estimated to have earned money.
          bannerAd.OnAdPaid += (AdValue adValue) =>
          {
               Debug.Log(String.Format("Banner view paid {0} {1}.",
                 adValue.Value,
                 adValue.CurrencyCode));
          };
          // Raised when an impression is recorded for an ad.
          bannerAd.OnAdImpressionRecorded += () =>
          {
               Debug.Log("Banner view recorded an impression.");
          };
          // Raised when a click is recorded for an ad.
          bannerAd.OnAdClicked += () =>
          {
               Debug.Log("Banner view was clicked.");
          };
          // Raised when an ad opened full screen content.
          bannerAd.OnAdFullScreenContentOpened += () =>
          {
               Debug.Log("Banner view full screen content opened.");
          };
          // Raised when the ad closed full screen content.
          bannerAd.OnAdFullScreenContentClosed += () =>
          {
               Debug.Log("Banner view full screen content closed.");
          };

     }
     #endregion
     #region Inter
     Action interComplete, interFail;
     public void LoadInterstitialAd()
     {
          if (interAd != null)
          {
               interAd.Destroy();
               interAd = null;
          }

          Debug.Log("Loading the interstitial ad.");

          // create our request used to load the ad.
          var adRequest = new AdRequest.Builder()
                  .AddKeyword("unity-admob-sample")
                  .Build();
          if (adTest) Inter_ID = "ca-app-pub-3940256099942544/1033173712"; ;
          // send the request to load the ad.
          InterstitialAd.Load(Inter_ID, adRequest,
              (InterstitialAd ad, LoadAdError error) =>
              {
                   // if error is not null, the load request failed.
                   if (error != null || ad == null)
                   {
                        Debug.LogError("interstitial ad failed to load an ad " +
                                     "with error : " + error);
                        return;
                   }

                   Debug.Log("Interstitial ad loaded with response : "
                           + ad.GetResponseInfo());

                   interAd = ad;
                   RegisterEventHandlersInter(ad);
              });
     }
     public void ShowInterstitialAd(Action _complete, Action _fail, string _placement)
     {
          interComplete = _complete;
          interFail = _fail;

          if (interAd != null && interAd.CanShowAd())
          {
               interAd.Show();
          }
          else
          {
               interFail?.Invoke();
          }
     }
     private void RegisterEventHandlersInter(InterstitialAd ad)
     {
          // Raised when the ad is estimated to have earned money.
          ad.OnAdPaid += (AdValue adValue) =>
          {
               Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                 adValue.Value,
                 adValue.CurrencyCode));
          };
          // Raised when an impression is recorded for an ad.
          ad.OnAdImpressionRecorded += () =>
          {
              
              Debug.Log("Interstitial ad recorded an impression.");
          };
          // Raised when a click is recorded for an ad.
          ad.OnAdClicked += () =>
          {
               Debug.Log("Interstitial ad was clicked.");
          };
          // Raised when an ad opened full screen content.
          ad.OnAdFullScreenContentOpened += () =>
          {
              // AnalyticManager.LogEventAdsInter();
               Debug.Log("Interstitial ad full screen content opened.");
          };
          // Raised when the ad closed full screen content.
          ad.OnAdFullScreenContentClosed += () =>
          {
               Debug.Log("Interstitial ad full screen content closed.");
               // Reload the ad so that we can show another as soon as possible.
               LoadInterstitialAd();
               interComplete?.Invoke();
          };
          // Raised when the ad failed to open full screen content.
          ad.OnAdFullScreenContentFailed += (AdError error) =>
          {
               Debug.LogError("Interstitial ad failed to open full screen content " +
                            "with error : " + error);
               // Reload the ad so that we can show another as soon as possible.
               LoadInterstitialAd();
               interFail?.Invoke();
          };
     }
     #endregion
     #region Reward
     Action completeReward;
     Action failReward;
     bool isReceivedReward;
     public void LoadRewardedAd()
     {
          // Clean up the old ad before loading a new one.
          if (rewardAd != null)
          {
               rewardAd.Destroy();
               rewardAd = null;
          }

          Debug.Log("Loading the rewarded ad.");

          // create our request used to load the ad.
          var adRequest = new AdRequest.Builder().Build();

          if (adTest) Reward_ID = "ca-app-pub-3940256099942544/5224354917";
          // send the request to load the ad.
          RewardedAd.Load(Reward_ID, adRequest,
              (RewardedAd ad, LoadAdError error) =>
              {
                   // if error is not null, the load request failed.
                   if (error != null || ad == null)
                   {
                        Debug.LogError("Rewarded ad failed to load an ad " +
                                     "with error : " + error);
                        return;
                   }

                   Debug.Log("Rewarded ad loaded with response : "
                           + ad.GetResponseInfo());

                   rewardAd = ad;
                   RegisterEventHandlersReward(ad);
              });
     }
     public void ShowRewardedAd(Action _complete, Action _fail, string _placement)
     {
          completeReward = _complete;
          failReward = _fail;
          const string rewardMsg =
              "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";


          if (rewardAd != null && rewardAd.CanShowAd())
          {

               rewardAd.Show((Reward reward) =>
               {
                    isReceivedReward = true;
                    // TODO: Reward the user.
                    Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
               });
          }
          else
          {
               failReward?.Invoke();
          }
     }
     private void RegisterEventHandlersReward(RewardedAd ad)
     {
          // Raised when the ad is estimated to have earned money.
          ad.OnAdPaid += (AdValue adValue) =>
          {
               Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                 adValue.Value,
                 adValue.CurrencyCode));
          };
          // Raised when an impression is recorded for an ad.
          ad.OnAdImpressionRecorded += () =>
          {
             

              Debug.Log("Rewarded ad recorded an impression.");
          };
          // Raised when a click is recorded for an ad.
          ad.OnAdClicked += () =>
          {
               Debug.Log("Rewarded ad was clicked.");
          };
          // Raised when an ad opened full screen content.
          ad.OnAdFullScreenContentOpened += () =>
          {
               //AnalyticManager.LogEventAdsReward();
               Debug.Log("Rewarded ad full screen content opened.");
          };
          // Raised when the ad closed full screen content.
          ad.OnAdFullScreenContentClosed += () =>
          {
               Debug.Log("Rewarded ad full screen content closed.");
               // Reload the ad so that we can show another as soon as possible.

               if (isReceivedReward)
               {
                    AdManager.Resume();
                    Invoke(nameof(DelayReward), .3f);
                    isReceivedReward = false;
               }
               LoadRewardedAd();
          };
          // Raised when the ad failed to open full screen content.
          ad.OnAdFullScreenContentFailed += (AdError error) =>
          {
               Debug.LogError("Rewarded ad failed to open full screen content " +
                            "with error : " + error);
               // Reload the ad so that we can show another as soon as possible.
               LoadRewardedAd();
               failReward?.Invoke();
          };
     }
     void DelayReward()
     {
          completeReward?.Invoke();
     }
     #endregion
}


