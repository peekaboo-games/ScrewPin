using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleBox : MonoBehaviour
{
    [SerializeField] TypeFace _typeFaceBox = TypeFace.CIRCLE;
    [SerializeField] bool _isHasScrew;
    public bool IsHasScrew
    {
        get
        {
            return _isHasScrew;
        }
        set
        {
            _isHasScrew = value;
        }
    }
}
