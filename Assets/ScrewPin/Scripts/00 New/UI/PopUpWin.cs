using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PopUpWin : GameMonobehavior
{
    [SerializeField] private Transform _pointPos;
    [SerializeField] private GameObject _winWarp;
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private TMP_Text _rewardLuckyWheelText;


    public float interAdsTime = 120f;
    private const string TimerKeyWin = "InterAdsTimer";
    private int coinReward = 50;

    private void Start()
    {
        interAdsTime = PlayerPrefs.GetFloat(TimerKeyWin, 120f);
        StartCoroutine(UpdateTimer());
        StartCoroutine(UpdateLuckyWheelText());

        UpdateRewardText();
    }


    public void UpdateRewardText()
    {
        _rewardText.text = "Reward " + coinReward.ToString();
    }
    private IEnumerator UpdateTimer()
    {
        while (true)
        {
            interAdsTime -= Time.deltaTime;
            PlayerPrefs.SetFloat(TimerKeyWin, interAdsTime);
            yield return null;
        }
    }
    public void NoThank()
    {
        Ac.PlaySound(Ac.click);
        Ac.StopPlaySfx(Ac.win);
        Ac.PlayBackgroundMusic();
        Sc.TransitionLoad();
        InscreasCoin(50);
        if (!Gm.LimitLevel())
        {
            Gm.LevelManager.LoadLevel();
        }
        else
        {
            SceneManager.LoadScene("GamePlay");
        }
        Gpm.ResetSkills();
        _winWarp.SetActive(false);


    }

    public void NoThankAdsInter()
    {

        if (interAdsTime <= 0)
        {
            AdManager.instance.ShowInter(() =>
            {
                ResetTimer();
                NoThank();
            },
            () =>
            {
                ResetTimer();
                NoThank();
            }, "Null");
        }
        else
        {
            NoThank();
            
        }
    }
    private void ResetTimer()
    {
        interAdsTime = 120f;
        PlayerPrefs.SetFloat(TimerKeyWin, interAdsTime);
    }

    public void AdsLuckyWheelReward()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            UIManager.Instance.ShowNoInternetPopUp();
            return;
        }
        AdManager.instance.ShowReward(() =>
        {
            GetReward();

        }, () =>
        {

        }, "YourPlacementID");
    }

    public void InscreasCoin(int amount)
    {
        Dtm.Gold += amount;
    }

    public void MultipleCoin(int amount, int coin)
    {
        coin *= amount;

        Dtm.Gold += coin;

    }


    public void GetReward()
    {
        Ac.PlaySound(Ac.click);
        Ac.StopPlaySfx(Ac.win);
        Ac.PlayBackgroundMusic();

        float rot = _pointPos.eulerAngles.z;

        if (rot > 180)
        {
            rot -= 360;
        }

        if (rot > 38 && rot <= 90)
        {
            Debug.Log("x3");
            MultipleCoin(3, 50);

        }
        else if (rot > -27 && rot <= 25)
        {
            Debug.Log("x2");
            MultipleCoin(2, 50);
        }
        else if (rot > -64 && rot <= -27)
        {


            Debug.Log("x5");
            MultipleCoin(5, 50);
        }
        else if (rot <= -64 && rot > -90)  
        {
            Debug.Log("x4");
            MultipleCoin(4, 50);
        }

        // load lv
        if (!Gm.LimitLevel())
        {
            Gm.LevelManager.LoadLevel();
        }
        else
        {
            SceneManager.LoadScene("GamePlay");
        }
        _winWarp.SetActive(false);

    }

    private IEnumerator UpdateLuckyWheelText()
    {
        while (true)
        {
            float rot = _pointPos.eulerAngles.z;

            if (rot > 180)
            {
                rot -= 360;
            }

            if (rot > 38 && rot <= 90)
            {
                _rewardLuckyWheelText.text = (coinReward * 2).ToString();
            }
            else if (rot > -27 && rot <= 25)
            {
                _rewardLuckyWheelText.text = (coinReward * 3).ToString();
            }
            else if (rot > -64 && rot <= -27)
            {
                _rewardLuckyWheelText.text = (coinReward * 4).ToString();
            }
            else if (rot <= -64 && rot > -90)
            {
                _rewardLuckyWheelText.text = (coinReward * 5).ToString();
            }
            yield return null;
        }

    }

}
