using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : GameMonobehavior
{
    public static GameManager Instance;
    [SerializeField] BoxController _boxController;
    [SerializeField] HoleStorageController _holeStorageController;
    [SerializeField] private LevelManager _levelManager;

    public LevelManager LevelManager => _levelManager;
    public BoxController BoxController { get => _boxController; set => _boxController = value; }
    public HoleStorageController HoleStorageController { get => _holeStorageController; set => _holeStorageController = value; }
    //public int levelPlaying;
    


    public bool cantClick;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DontDestroyOnLoad(gameObject);
        cantClick = true;
        
    }


    private void Start()
    {
        Observe.OnWin += OnWin;
        Observe.OnLose += OnLose;
    }
    public void ReloadCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void OnWin()
    {
        Debug.Log("onWin");
        IncrseaseLevel();
        StartCoroutine(UIManager.Instance.ShowWinPopup());

    }

    public bool LimitLevel()
    {
        if(Dtm.Level >= Gc.levelConfig.GetTotalLevel())
        {
            return true;
        }
        return false;
    }
    public void OnLose()
    {

        StartCoroutine(UIManager.Instance.ShowLosePopup());

    }
   
    public void IncrseaseLevel()
    {
        if (Dtm.Level > Gc.levelConfig.GetTotalLevel()) return;
        int indexLevel = Dtm.Level + 1;
        
        Dtm.SetLevel(indexLevel);
    }
    private void OnDestroy()
    {
        Observe.OnWin -= OnWin;
        Observe.OnLose -= OnLose;
    }

}
