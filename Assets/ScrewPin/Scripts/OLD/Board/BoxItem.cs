using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class BoxItem : GameMonobehavior
{
    [SerializeField] TypeColor _typeColor;
    [SerializeField] Transform _lid;
    [SerializeField] HoleBox _holeBoxPrefab;
    [SerializeField] List<HoleBox> _holeBoxes = new List<HoleBox>();
    public int totalHoleBoxItem;
    public TypeColor TypeColor => _typeColor;
    private void Awake()
    {
        _lid.gameObject.SetActive(false);
        _lid.localPosition = Vector3.up * 8;
        var holes = GetComponentsInChildren<HoleBox>();
        foreach (var item in holes)
        {
            _holeBoxes.Add(item);
        }
        totalHoleBoxItem = _holeBoxes.Count;
    }

    public void NextBox(float posX, System.Action callback = null)
    {
        transform.DOLocalMoveX(transform.position.x + posX, 0.5f)
            .OnComplete(() =>
            {
                callback?.Invoke();
            });
    }
    public bool CheckBoxFull()
    {
        foreach (var item in _holeBoxes)
        {
            if (!item.IsHasScrew)
                return false;
        }
        return true;
    }
    public Transform GetHole()
    {
        foreach (var item in _holeBoxes)
        {
            if (!item.IsHasScrew)
            {
                item.IsHasScrew = true;
                return item.transform;
            }
        }

        return null;
    }
    public void LidFall()
    {
        _lid.gameObject.SetActive(true);
        _lid.DOLocalMoveY(0, 0.5f).OnComplete(() =>
        {
            Ac.PlaySound(Ac.closeLid);
            Gm.BoxController.MoveDone();
        });
    }
    public void AutoCatchScrew()
    {
        foreach (var item in Gm.HoleStorageController.Holes)
        {
            if (item.ScrewItem != null && item.ScrewItem.TypeColor == _typeColor) // Kiểm tra ScrewItem không null
            {
                Debug.Log("AutoCatchScrew");
                item.ScrewItem.MoveToTarget(true, hole: item);
/*              item.ResetHole();
*/            }
        }
    }
}
