using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Data SO Box", fileName = "Data Box")]
public class DataSOBox : ScriptableObject
{
    [SerializeField] private List<BoxModel> _listBoxModel = new();
    public List<BoxModel> ListBoxModel => _listBoxModel;
    public BoxItem GetBoxItemOfType(TypeColor type)
    {
        foreach (var item in _listBoxModel)
        {
            if(item.TypeColorBox == type)
            {
                return item.BoxItem;
            }
        }
        return null;
    }
}
[System.Serializable]
public class BoxModel
{
    public TypeColor TypeColorBox;
    public BoxItem BoxItem;
}
