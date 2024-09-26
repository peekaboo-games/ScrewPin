using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class LevelManager : GameMonobehavior
{
    private GameObject _objLevel;

    [NotNull] private LevelConfig _levelConfig;

    private void Start()
    {
        LoadLevel();
        _levelConfig = GameConfig.Instance.levelConfig;
    }
    public void LoadLevel()
    {
        StartCoroutine(SpawnLevel());

    }

    public void LoadLevelByIndex(int indexLevel)
    {

        StartCoroutine(SpawnLevel(indexLevel));

    }

    IEnumerator SpawnLevel(int index = 0)
    {
        //Debug.Log(_currentLevel + "currentlevel");
        if (_objLevel != null)
        {
            Destroy(_objLevel);
        }
        GameObject _currentLevel;

        yield return new WaitForSeconds(0.5f);

        if (index == 0)
        {
            Dtm.Level = Dtm.GetLevel();
            _currentLevel = _levelConfig.GetLevelByIndex(Dtm.Level - 1);
        }
        else
        {
            Dtm.Level = index;
            _currentLevel = _levelConfig.GetLevelByIndex(index);
        }

        _objLevel = Instantiate(_currentLevel);


    }
}
