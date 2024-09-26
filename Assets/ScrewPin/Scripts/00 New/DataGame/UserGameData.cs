using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ES3Serializable]
public class UserGameData
{
    [ES3Serializable]
    private Dictionary<ResourceType, int> gameResources;
    public Dictionary<ResourceType, int> GameResources { get => gameResources; }

    [ES3Serializable]
    private DataLevel dataLevel;
    private bool isFirstInGame = false;
    private string version_game;

    public UserGameData()
    {
        gameResources = new Dictionary<ResourceType, int>();
        DefaultFirstInGame();
    }

    private void DefaultFirstInGame()
    {
        if (!isFirstInGame)
        {
            if (dataLevel == null)
            {
                dataLevel = new DataLevel();
                dataLevel.CurrentLevel = 1;
                gameResources.Add(ResourceType.RemoveAds, 0);
                gameResources.Add(ResourceType.Gold, 100);
                gameResources.Add(ResourceType.Life, 5);
                gameResources.Add(ResourceType.Level, 1);


                isFirstInGame = true;
                version_game = Application.version.ToString(); 
            }
        }
    }

    public int GetLevelGame()
    {
        return dataLevel.GetLevel();
    }

    public void SetLevelGame(int level)
    {
        dataLevel.SetLevel(level);
    }


}

public class DataLevel
{

    public int CurrentLevel;

    public void SetLevel(int level)
    {
        CurrentLevel = level;
    }

    public int GetLevel()
    {
        return CurrentLevel;
    }


}
