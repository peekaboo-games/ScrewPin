using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Data SO Screw", fileName = "Data Screw")]
public class DataSoScrew : ScriptableObject
{
    [SerializeField] private List<ScrewModel> _listScrewModel = new();
    public List<ScrewModel> ListScrewModel => _listScrewModel;
    public ScrewItem GetScrewItemOfType(TypeColor type)
    {
        foreach (var item in _listScrewModel)
        {
            if (item.TypeColorScrew == type)
            {
                return item.ScrewItem;
            }
        }
        return null;
    }
}
[System.Serializable]
public class ScrewModel
{
    public TypeColor TypeColorScrew;
    public ScrewItem ScrewItem;
}
