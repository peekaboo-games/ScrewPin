using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private UserGameData _userGameData;
    private const string USER_DATA_FILE = "user_game_screw_data";
    private ES3Settings USER_GAME_DATA_SETTINGS;
    private const string DATA_KEY = "data_game_screw";
    private Dictionary<ResourceType, List<Action<ResourceType>>> onResourceTypeChanged = new();
    public UserGameData UserGameData { get => _userGameData; set => _userGameData = value; }
    public event Action<ResourceType, bool> OnResourceTypeChangedSuspend;


    protected void Awake()
    {
        USER_GAME_DATA_SETTINGS = new ES3Settings(ES3.EncryptionType.AES, "kdfj3485j");
        USER_GAME_DATA_SETTINGS.location = ES3.Location.File;

        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
        DontDestroyOnLoad(this);
        
    }

    private IEnumerator Start()
    {
        yield return null;
        LoadUserGameData();
    }

    private void LoadUserGameData()
    {
        Debug.Log("USER_DATA_FILE: " + USER_DATA_FILE);
        if (ES3.KeyExists(DATA_KEY, USER_DATA_FILE, USER_GAME_DATA_SETTINGS))
        {
            Debug.Log("Load Data");
            _userGameData = ES3.Load<UserGameData>(DATA_KEY, USER_DATA_FILE, USER_GAME_DATA_SETTINGS);
        }
        else
        {
            Debug.Log("Save Data");
            _userGameData = new();
            SaveUserGameData();
        }

        if (_userGameData == null)
        {
        }
    }


    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveUserGameData();
        }
    }

    public int GetLevel()
    {
        
        return _userGameData.GetLevelGame();
    }
    public void SetLevel(int index)
    {
       
       _userGameData.SetLevelGame(index);
        
    }

    public void SaveUserGameData()
    {
        if (_userGameData != null)
        {
            ES3.Save<UserGameData>(DATA_KEY, _userGameData, USER_DATA_FILE, USER_GAME_DATA_SETTINGS);
        }
    }

    public void RegisterResourceTypeChangedListener(ResourceType type, Action<ResourceType> listener)
    {
        List<Action<ResourceType>> actions = GetResourceTypeChangedListeners(type);

        if (!actions.Contains(listener))
        {
            actions.Add(listener);
        }
    }

    public void UnregisterResourceTypeChangedListener(ResourceType type, Action<ResourceType> listener)
    {
        List<Action<ResourceType>> actions = GetResourceTypeChangedListeners(type);

        if (actions.Contains(listener))
        {
            actions.Remove(listener);
        }
    }

    private List<Action<ResourceType>> GetResourceTypeChangedListeners(ResourceType type)
    {
        List<Action<ResourceType>> actions = null;
        if (onResourceTypeChanged.ContainsKey(type))
        {
            actions = onResourceTypeChanged[type];
        }
        else
        {
            actions = new();
            onResourceTypeChanged[type] = actions;
        }

        return actions;
    }

    private void InvokeResourceTypeChangedListener(ResourceType type)
    {
        List<Action<ResourceType>> actions = GetResourceTypeChangedListeners(type);
        foreach (Action<ResourceType> action in actions)
        {
            action?.Invoke(type);
        }
    }

    public int Gold
    {
        get { return GetResourceTypeCount(ResourceType.Gold); }
        set
        {
            SetResourceTypeCount(ResourceType.Gold, value);
        }
    }

    public int Life
    {
        get { return GetResourceTypeCount(ResourceType.Life); }
        set
        {
            SetResourceTypeCount(ResourceType.Life, value);
        }
    }

    public int Level
    {
        get { return GetResourceTypeCount(ResourceType.Level); }
        set
        {
            SetResourceTypeCount(ResourceType.Level, value);
        }
    }
    public bool RemoveAds
    {
        get { return GetResourceTypeCount(ResourceType.RemoveAds) >= 1; }
        set
        {
            SetResourceTypeCount(ResourceType.RemoveAds, value ? 1 : 0);
        }
    }

 /*   public void ClaimResourceItems(List<ResourceItem> resourceItems)
    {
        foreach (ResourceItem resourceItem in resourceItems)
        {
            int curValue = GetResourceTypeCount(resourceItem.type);
            curValue += resourceItem.quatity;
            SetResourceTypeCount(resourceItem.type, curValue);
        }
    }*/

    public int GetResourceTypeCount(ResourceType resourceType)
    {
        return _userGameData.GameResources[resourceType];
    }

    public void SetResourceTypeCount(ResourceType resourceType, int value)
    {
        _userGameData.GameResources[resourceType] = value;
        InvokeResourceTypeChangedListener(resourceType);
    }

    public void SupspendResourceTypeChanged(ResourceType resourceType)
    {
        OnResourceTypeChangedSuspend?.Invoke(resourceType, true);
    }

    public void ResumeResourceTypeChanged(ResourceType resourceType)
    {
        OnResourceTypeChangedSuspend?.Invoke(resourceType, false);
    }

    public void TriggerResourceTypeChanged(ResourceType resourceType)
    {
        InvokeResourceTypeChangedListener(resourceType);
    }
}

