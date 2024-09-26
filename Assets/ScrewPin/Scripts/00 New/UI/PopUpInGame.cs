using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpInGame : GameMonobehavior
{
    [SerializeField] private TMP_Text _lifeText;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _levelText;


    [SerializeField] private int _cost1 = 0;
    [SerializeField] private int _cost2 = 0;
    [SerializeField] private int _cost3 = 0;

    [SerializeField] private TMP_Text _costSupText1;
    [SerializeField] private TMP_Text _costSupText2;
    [SerializeField] private TMP_Text _costSupText3;

    [SerializeField] private GameObject _coinPanel1;
    [SerializeField] private GameObject _coinPanel2;
    [SerializeField] private GameObject _coinPanel3;

    [SerializeField] private GameObject _freePanel1;
    [SerializeField] private GameObject _freePanel2;
    [SerializeField] private GameObject _freePanel3;


    public float interAdsTime = 120f;
    private const string TimerKeyReload = "InterAdsTimer";

    private void OnEnable()
    {
        Dtm.RegisterResourceTypeChangedListener(ResourceType.Life, UpdateTextLife);
        Dtm.RegisterResourceTypeChangedListener(ResourceType.Gold, UpdateTextCoin);
        Dtm.RegisterResourceTypeChangedListener(ResourceType.Level, UpdateLevelText);

    }

    private void OnDisable()
    {
        Dtm.UnregisterResourceTypeChangedListener(ResourceType.Life, UpdateTextLife);
        Dtm.UnregisterResourceTypeChangedListener(ResourceType.Gold, UpdateTextCoin);
        Dtm.RegisterResourceTypeChangedListener(ResourceType.Level, UpdateLevelText);

    }
    public void UpdateTextCoin(ResourceType resourceType)
    {
        _coinText.text = Dtm.Gold.ToString();
        Debug.Log(Dtm.Gold + " " + resourceType);
    }

    public void UpdateTextLife(ResourceType resourceType)
    {
        _lifeText.text = Dtm.Life.ToString();

    }
    private void Start()
    {
        UpdateTextLife(ResourceType.Life);
        UpdateTextCoin(ResourceType.Gold);
        UpdateLevelText(ResourceType.Level);
        UpdateCostText();
        //interAdsTime = PlayerPrefs.GetFloat(TimerKeyReload, 120f);
        //StartCoroutine(UpdateTimer());
        StartCoroutine(UpdatePanel());
    }
    private IEnumerator UpdateTimer()
    {
        while (true)
        {
            interAdsTime -= Time.deltaTime;
            PlayerPrefs.SetFloat(TimerKeyReload, interAdsTime);
            yield return null;
        }
    }

    private IEnumerator UpdatePanel()
    {
        while (true)
        {
            // sup 1
            if (Dtm.Gold < _cost1)
            {
                _freePanel1.SetActive(true);
                _coinPanel1.SetActive(false);
            }
            if (Dtm.Gold >= _cost1)
            {
                _freePanel1.SetActive(false);
                _coinPanel1.SetActive(true);
            }
            // sup 2
            if (Dtm.Gold < _cost2)
            {
                _freePanel2.SetActive(true);
                _coinPanel2.SetActive(false);

            }
            if (Dtm.Gold >= _cost2)
            {
                _freePanel2.SetActive(false);
                _coinPanel2.SetActive(true);
            }
            //sup 3
            if (Dtm.Gold < _cost3)
            {
                _freePanel3.SetActive(true);
                _coinPanel3.SetActive(false);

            }
            if (Dtm.Gold >= _cost3)
            {
                _freePanel3.SetActive(false);
                _coinPanel3.SetActive(true);
            }

            yield return null;
        }
    }
    private void UpdateCostText()
    {
        _costSupText1.text = _cost1.ToString();
        _costSupText2.text = _cost2.ToString();
        _costSupText3.text = _cost3.ToString();

    }
    public void DescreaCoin(int amount)
    {
        Dtm.Gold -= amount;
    }
    public void AddHole()
    {

        Gpm.OnAddHole?.Invoke();
        Gpm.UseSkill("AddHole");

        Ac.PlaySound(Ac.click);

    }
    public void Shuffle()
    {

        Gpm.OnShuffle?.Invoke();
        //Gpm.UseSkill("Shuffle");

        Ac.PlaySound(Ac.click);


    }

    public void AutoMatch()
    {
        Gpm.OnAutoMatch?.Invoke();
        //Gpm.UseSkill("AutoMatch");

        Ac.PlaySound(Ac.click);

    }

    public void UpdateLevelText(ResourceType resourceType)
    {
        _levelText.text = "LEVEL " + Dtm.Level.ToString();
    }

    public void Replay()
    {
        SceneManager.LoadScene("GamePlay");
        Sc.TransitionLoad();
        DescreaseLife(1);

    }

    public void DescreaseLife(int amount)
    {
        if (Dtm.Life <= 0) return;
        Dtm.Life -= amount;
    }
    /*public void ReplayAds()
    {
        if (interAdsTime <= 0)
        {
            AdManager.instance.ShowInter(() =>
            {
                Replay();
                ResetTimer();

            },
            () =>
            {
                Replay();
                ResetTimer();

            }, "Null");
        }
        else
        {
            Replay();

        }
    }*/
    private void ResetTimer()
    {
        interAdsTime = 120f;
        PlayerPrefs.SetFloat(TimerKeyReload, interAdsTime);
    }


    public void AdNewHoleReward()
    {
        if (Dtm.Gold < _cost1)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                UIManager.Instance.ShowNoInternetPopUp();
                return;
            }
            Debug.Log("Not enough gold to add a hole.");
            AdManager.instance.ShowReward(() =>
            {
                Ac.PlaySound(Ac.click);
                AddHole();
                //ResetTimer();

            }, () =>
            {
                //ResetTimer();

            }, "YourPlacementID");

        }
        else
        {
            Ac.PlaySound(Ac.click);
            AddHole();
            DescreaCoin(_cost1);
        }
    }
    public void AdShuffleReward()
    {
        if (Dtm.Gold < _cost2)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                UIManager.Instance.ShowNoInternetPopUp();
                return;
            }
            AdManager.instance.ShowReward(() =>
            {
                Ac.PlaySound(Ac.click);
                //ResetTimer();

                Shuffle();

            }, () =>
            {
                //ResetTimer();

            }, "YourPlacementID");
        }
        else
        {
            Ac.PlaySound(Ac.click);
            DescreaCoin(_cost2);
            Shuffle();
        }

    }

    public void AdAutoMatchReward()
    {
        if (Dtm.Gold < _cost3)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                UIManager.Instance.ShowNoInternetPopUp();
                return;
            }
            AdManager.instance.ShowReward(() =>
            {
                Ac.PlaySound(Ac.click);
                //ResetTimer();

                AutoMatch();

            }, () =>
            {
                //ResetTimer();


            }, "YourPlacementID");
        }
        else
        {
            Ac.PlaySound(Ac.click);
            DescreaCoin(_cost3);
            AutoMatch();
        }

    }

}
