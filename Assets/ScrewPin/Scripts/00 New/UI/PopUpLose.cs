using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpLose : GameMonobehavior
{
    public float interAdsTime = 120f;
    private const string TimerKeyLose = "InterAdsTimer";

    [SerializeField] private GameObject loseWarp;

    private void Start()
    {
        interAdsTime = PlayerPrefs.GetFloat(TimerKeyLose, 120f);
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (true)
        {
            interAdsTime -= Time.deltaTime;
            PlayerPrefs.SetFloat(TimerKeyLose, interAdsTime);
            yield return null;
        }
    }
    public void SkipLevel()
    {
        Gm.cantClick = false;
        Ac.PlaySound(Ac.click);
        Ac.StopPlaySfx(Ac.lose);
        Ac.PlayBackgroundMusic();
        Sc.TransitionLoad();
        Gm.IncrseaseLevel();
        if (!Gm.LimitLevel())
        {
            Gm.LevelManager.LoadLevel();
        }
        else
        {
            Debug.Log("load lai home");
        }
        loseWarp.SetActive(false);
    }

    public void AdSkipReward()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            UIManager.Instance.ShowNoInternetPopUp();
            return;
        }
        AdManager.instance.ShowReward(() =>
        {
            SkipLevel();

        }, () =>
        {

        }, "YourPlacementID");
    }

    public void TryAgain()
    {
        Gm.cantClick = false;
        DescreaseLife(1);
        SceneManager.LoadScene("GamePlay");
        Ac.PlaySound(Ac.click);
        Ac.StopPlaySfx(Ac.lose);
        Ac.PlayBackgroundMusic();
        Sc.TransitionLoad();
    }
    

    public void TryAgainAdsInter()
    {
        if (interAdsTime <= 0)
        {
            AdManager.instance.ShowInter(() =>
            {
                ResetTimer();
                TryAgain();
            },
            () => {
                ResetTimer();
                TryAgain();
            }, "Null");
        }
        else
        {
            TryAgain();
        }
    }

    private void ResetTimer()
    {
        interAdsTime = 120f;
        PlayerPrefs.SetFloat(TimerKeyLose, interAdsTime);
    }
    public void DescreaseLife(int amount)
    {
        if (Dtm.Life <= 0) return;
        Dtm.Life -= amount;
    }
    
}
