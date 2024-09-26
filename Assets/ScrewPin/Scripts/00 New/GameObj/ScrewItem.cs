using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScrewInfo;

public class ScrewItem : GameMonobehavior
{
    [SerializeField] private TypeColor _typeColor;
    [SerializeField] private TypeFace _typeFace;
    [SerializeField] private BarItem _barItem;
    //[SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private HingeJoint _hingJoint;
    private bool isMouseDown = false;
    public bool isHole = false;
    public int indexHole = 0;
    private BoxCollider boxcolider;
    public MaskObject maskObject;

    public HingeJoint HingerJoint => _hingJoint;
    public TypeColor TypeColor => _typeColor;
    private void Awake()
    {
        _barItem = GetComponentInParent<BarItem>();
        boxcolider = GetComponent<BoxCollider>();
    }

    public void CheckCanUp()
    {
        Debug.Log("Can Up");
    }

    public void MoveToTarget(bool isOnHole = false, bool isBreakLimitBox = false,
        HoleStorageItem hole = null)
    {

        // Gm.HoleStorageController.ResetHoleHasScrews(indexHole);
        //maskObject.isHasScrew =  false;
        Debug.Log("AutoCatchScrew isBreakLimitBox " + isBreakLimitBox);
        if (_typeColor == Gm.BoxController.CurrentBox.TypeColor)
        {
            Gm.BoxController.SetParentScrewInBox(transform, this, isBreakLimitBox,
                MoveScrewTween, hole: hole);
        }
        else
        {
            Debug.Log("Move in hole");
            if (isOnHole) return;
            transform.SetParent(Gm.HoleStorageController.GetHoleStorageEmpty(this));
            MoveScrewTween(true);
        }
    }

    private void MoveScrewTween(bool isMove)
    {
        if (!isMove) return;
        if (boxcolider != null)
        {
            boxcolider.enabled = false;
        }
        DestroyImmediate(HingerJoint);
        Sequence sequence = DOTween.Sequence();
        sequence
            .Append(transform.DOLocalMove(new Vector3(0f, 0f, -1.5f), 0.25f))
            .Join(transform.DORotate(new Vector3(45f, 0f, 180f), 0.25f))
            .Append(transform.DORotate(Vector3.zero, 0.25f))
            .Append(transform.DOLocalMove(Vector3.zero, 0.25f))
            .OnComplete(() =>
        {
            if (Gm.BoxController.CurrentBox.CheckBoxFull())
            {
                Gm.BoxController.CurrentBox.LidFall();
            }
        });
    }
}
