using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleStorageItem : MonoBehaviour
{
    [SerializeField] bool _isHasCrew;
    [SerializeField] ScrewItem _screwItem;
    public ScrewItem ScrewItem => _screwItem;
    public bool IsHasScrew
    {
        get
        {
            return _isHasCrew;
        }
        set
        {
            _isHasCrew = value;
        }
    }
    public void SetScrewItem(ScrewItem screw)
    {
        _screwItem = screw;
    }
    public void ResetHole()
    {
        _screwItem = null;
        _isHasCrew = false;
    }
}
