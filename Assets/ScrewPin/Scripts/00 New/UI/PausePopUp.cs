using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PausePopUp : GameMonobehavior
{
    [SerializeField] private GameObject _musicOnBtn;
    [SerializeField] private GameObject _musicOffBtn;

    [SerializeField] private GameObject _sfxOnBtn;
    [SerializeField] private GameObject _sfxOffBtn;

    [SerializeField] private string gameUrl1 = "https://play.google.com/store/apps/details?id=com.cmgame.lifting.superhero.gym.clicker";
    [SerializeField] private string gameUrl2 = "https://play.google.com/store/apps/details?id=com.cmgame.gym.clicker.invincible.hero";
    

    private void Awake()
    {
        LoadButtonStates();
    }
    public void ResumeGame()
    {
        Ac.PlaySound(Ac.closePopup);
        Gm.cantClick = false;
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackMenu()
    {
        Ac.PlaySound(Ac.closePopup);
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        Sc.TransitionLoad();
    }

    public void ToggleMusicOn()
    {
        _musicOnBtn.SetActive(false);
        _musicOffBtn.SetActive(true);
        Ac.ToggleMusic();
        SaveButtonStates();
    }

    public void ToggleMusicOff()
    {
        _musicOnBtn.SetActive(true);
        _musicOffBtn.SetActive(false);
        Ac.ToggleMusic();
        SaveButtonStates();
    }

    public void ToggleSfxOn()
    {
        _sfxOnBtn.SetActive(false);
        _sfxOffBtn.SetActive(true);
        Ac.ToggleSFX();
        SaveButtonStates();
    }

    public void ToggleSfxOff()
    {
        _sfxOnBtn.SetActive(true);
        _sfxOffBtn.SetActive(false);
        Ac.ToggleSFX();
        SaveButtonStates();
    }
    public void MoreGame1()
    {
        Application.OpenURL(gameUrl1);
    }
    public void MoreGame2()
    {
        Application.OpenURL(gameUrl2);
    }
    private void SaveButtonStates()
    {
        PlayerPrefs.SetInt("MusicOnBtnActive", _musicOnBtn.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("MusicOffBtnActive", _musicOffBtn.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("SfxOnBtnActive", _sfxOnBtn.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("SfxOffBtnActive", _sfxOffBtn.activeSelf ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadButtonStates()
    {
        _musicOnBtn.SetActive(PlayerPrefs.GetInt("MusicOnBtnActive", 1) == 1);
        _musicOffBtn.SetActive(PlayerPrefs.GetInt("MusicOffBtnActive", 0) == 1);
        _sfxOnBtn.SetActive(PlayerPrefs.GetInt("SfxOnBtnActive", 1) == 1);
        _sfxOffBtn.SetActive(PlayerPrefs.GetInt("SfxOffBtnActive", 0) == 1);
    }
}
