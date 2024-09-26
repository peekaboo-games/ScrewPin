using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public static GameConfig Instance;
    public LevelConfig levelConfig;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
        DontDestroyOnLoad(this);
        Application.targetFrameRate = 60;
    }


}
