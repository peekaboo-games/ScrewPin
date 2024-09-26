
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class AdManager : MonoBehaviour
{
    public static AdManager instance { get; set; }
    private AdmobManager mediation;
    public float DefaultTimeInter;
    bool canShowOpenAd = true;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
            {
                Destroy(gameObject);
            }
        }
        mediation = GetComponent<AdmobManager>();
    }
    /*public Image loadingFill;*/
    private void Start()
    {

        // Create a ConsentRequestParameters object.
        GoogleMobileAds.Ump.Api.ConsentRequestParameters request = new GoogleMobileAds.Ump.Api.ConsentRequestParameters();

        // Check the current consent information status.
        GoogleMobileAds.Ump.Api.ConsentInformation.Update(request, OnConsentInfoUpdated);


        Invoke(nameof(StartLoading), 1f);

    }
    private void StartLoading()
    {
        /*        loadingFill.transform.parent.gameObject.SetActive(tr//ue);
                loadingFill.DOFillAmount(1f, 6f).OnComplete(() =>
               {
                   SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
               }).SetEase(Ease.Linear);*/
    }
    public static void PauseGame()
    {
        AudioListener.volume = 0;
        Time.timeScale = 0f;
    }
    public static void Resume()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1;
    }
    void OnConsentInfoUpdated(GoogleMobileAds.Ump.Api.FormError consentError)
    {
        if (consentError != null)
        {
            // Handle the error.
            UnityEngine.Debug.LogError(consentError);
            Init();
            return;
        }

        // If the error is null, the consent information state was updated.
        // You are now ready to check if a form is available.
        GoogleMobileAds.Ump.Api.ConsentForm.LoadAndShowConsentFormIfRequired((GoogleMobileAds.Ump.Api.FormError formError) =>
        {
            if (formError != null)
            {
                // Consent gathering failed.
                UnityEngine.Debug.LogError(consentError);
                Init();
                return;
            }

            // Consent has been gathered.
            if (GoogleMobileAds.Ump.Api.ConsentInformation.CanRequestAds())
            {
                Init();
            }
        });
    }


    public void Init()
    {
        mediation.Init();
        Invoke(nameof(ShowOpen), 3f);
    }
    public void ShowOpen()
    {
        if (PlayerPrefs.GetInt("RemoveAds", 0) == 0)
            mediation.ShowOpenAd();
       // if(!Application.isEditor)
        //InAppUpdate.getInstance();
    }
    public void ShowInter(Action complete, Action fail, string placement)
    {
        canShowOpenAd = false;
        AdManager.PauseGame();
        complete += () =>
        {
            AdManager.Resume();
         
            //UIController.i.TriggerWhenShowAds();
            Invoke(nameof(DelayOpenAds), 0.5f);
            //AnalyticManager.LogEventAdsInter();
        };
        fail += () =>
        {
            AdManager.Resume();
            Time.timeScale = 1f;
            //UIController.i.TriggerWhenShowAds();
            Invoke(nameof(DelayOpenAds), 0.5f);
        };

        if (PlayerPrefs.GetInt("RemoveAds", 0) == 0)
            mediation.ShowInterstitialAd(complete, fail, placement);
    }
    public void ShowReward(Action complete, Action fail, string placement)
    {
        canShowOpenAd = false;

        AdManager.PauseGame();

        complete += () =>
        {

            //UIController.i.TriggerWhenShowAds();
          

            Invoke(nameof(DelayOpenAds), 1f);
            //AnalyticManager.LogEventAdsReward();
            //AnalyticManager.LogEventContent(placement);
        };
        fail += () =>
        {
            AdManager.Resume();
            // UIController.i.TriggerWhenShowAds();
            Invoke(nameof(DelayOpenAds), 0.5f);
        };
        mediation.ShowRewardedAd(complete, fail, placement);
    }
    public void ShowBanner()
    {
        if (PlayerPrefs.GetInt("RemoveAds", 0) == 0)
            mediation.LoadBanner();
    }
    public void HideBanner()
    {
        mediation.HideBanner();
    }
    private void DelayOpenAds() => canShowOpenAd = true;

    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus && canShowOpenAd)
        {
            ShowOpen();
        }
    }
}
