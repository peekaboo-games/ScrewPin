using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : GameMonobehavior
{
    //Ui
    [SerializeField] private GameObject _loseUi;
    [SerializeField] private GameObject _winUi;
    [SerializeField] private GameObject _inGameUi;
    //popup
    [SerializeField] private GameObject _settingPopup;
    [SerializeField] private GameObject _pausePopup;
    [SerializeField] private GameObject _buyReturnPopup;
    [SerializeField] private GameObject _NoIneternetPopup;


    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
    }
    public IEnumerator ShowLosePopup()
    {
        yield return new WaitForSeconds(1f);
        Ac.PlaySound(Ac.lose);
        Ac.StopPlayMusic();
        _loseUi.SetActive(true);
    }

    public IEnumerator ShowWinPopup()
    {

        yield return new WaitForSeconds(1f);
        Ac.PlaySound(Ac.win);
        Ac.StopPlayMusic();
        _winUi.SetActive(true);
    }

    public void ShowNoInternetPopUp()
    {

        
        _NoIneternetPopup.SetActive(true);
        

    }

    public void ShowSettingPopup()
    {
        Ac.PlaySound(Ac.openPopup);
        Gm.cantClick = true;
        _settingPopup.SetActive(true);
        Time.timeScale = 0f;

    }

    public void ShowPausePopup()
    {
        Ac.PlaySound(Ac.openPopup);
        Gm.cantClick = true;

        _pausePopup.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowBuyReturnPopup()
    {
        Ac.PlaySound(Ac.openPopup);
        Gm.cantClick = true;


        _buyReturnPopup.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowInGameUi()
    {
        _inGameUi.SetActive(true);
    }
}
