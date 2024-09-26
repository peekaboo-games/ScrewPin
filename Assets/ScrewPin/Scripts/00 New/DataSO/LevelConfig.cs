using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;


[CreateAssetMenu(menuName = "Data/Data SO Level", fileName = "Data Level")]

public class LevelConfig : ScriptableObject
{
    public List<GameObject> levels = new();
    
    public int GetTotalLevel()
    {
        return levels.Count;
    }

    public GameObject GetLevelByIndex(int index)
    {
        if (index < 0 || index > levels.Count) return levels[0];
        return levels[index];
    }
   

#if UNITY_EDITOR
    [Button("Load level Obj By Path")]
    public void LoadLevelObjByPath()
    {
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { GameConstrain.PATH_LEVEL_PREFAB });
        levels.Clear();
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            levels.Add(prefab);
        }

    }
#endif

}
