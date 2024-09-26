using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum ETypeBox
{
    NONE = -1,
    BOX_1,
    BOX_2,
    BOX_3,
    BOX_4,
    BOX_5,
}
public class BoxController : GameMonobehavior
{
    [SerializeField] BoxItem _currentBox;
    [SerializeField] DataSOBox _dataSOBox;
    [SerializeField] List<BoxItem> _listBoxes;
    public BoxItem CurrentBox => _currentBox;
    private bool isLastBox = false;
    public int totalHole = 0;
    public int countScrewInHoleBox = 0;

    [Header("Configs")]
    [SerializeField] float _spacingX;
    #region Tools
#if UNITY_EDITOR
    [Header("Generate Box")]
    [SerializeField] List<TypeColor> _listTypeBoxGenerate = new List<TypeColor>();
    [Button("Generate")]
    private void GenerateLevel()
    {
        foreach (var item in _listTypeBoxGenerate)
        {
            BoxItem boxClone = Instantiate(_dataSOBox.GetBoxItemOfType(item));
            boxClone.transform.SetParent(transform);
            boxClone.transform.localPosition = Vector3.zero;
            _listBoxes.Add(boxClone);

        }
    }
#endif
    [Button("Fitter Horizontal")]
    private void AutoFitterHorizontal()
    {
        float posX = 0;
        foreach (var item in _listBoxes)
        {
            item.transform.localPosition = Vector3.left * posX;
            posX += _spacingX;
        }
    }
    #endregion

    public void SetParentScrewInBox(Transform transform, ScrewItem screw, bool isBreakLimitBox = false,
        Action<bool> callback = null, HoleStorageItem hole = null)
    {
        countScrewInHoleBox++;
        Debug.Log("isBreakLimitBox: " + isBreakLimitBox);
        if (isBreakLimitBox)
        {
            if (countScrewInHoleBox > totalHole)
            {
                Debug.Log("Move test");
                callback?.Invoke(false);
                if(hole != null)
                {
                    hole.ResetHole();
                }
                return;
            }
        }
/*        callback?.Invoke(true);
*/
        if (countScrewInHoleBox <= totalHole)
        {
            Debug.Log("Move In Box");
            screw.isHole = true;
            transform.SetParent(CurrentBox.GetHole());
            callback?.Invoke(true);
            if (hole != null)
            {
                hole.ResetHole();
            }
        }
        /*else
        {
            Debug.Log("Move In hole");
            transform.SetParent(Gm.HoleStorageController.GetHoleStorageEmpty(screw));
        }*/

    }

    private void Awake()
    {
        Gm.BoxController = this;
    }

    private void Start()
    {
        InitCurrentBox(_listBoxes[0]);
    }
    public void InitCurrentBox(BoxItem box)
    {
        _currentBox = box;
        totalHole = _currentBox.totalHoleBoxItem;
        countScrewInHoleBox = 0;
    }
    public void MoveDone()
    {
        _listBoxes.Remove(_currentBox);
        // Disable screw clicking
        Iph.SetClickability(false);
        Ac.PlaySound(Ac.moveBox);
        _currentBox.NextBox(_spacingX, () =>
        {
            MoveAllBox();
            CurrentBox.AutoCatchScrew(); // Gọi AutoCatchScrew() sau khi di chuyển tất cả các hộp

            // Re-enable screw clicking
            Iph.SetClickability(true);
        });
        if (_listBoxes.Count == 0)
        {
            if (isLastBox) return;
            isLastBox = true;
            Debug.Log("Win");
            Observe.OnWin?.Invoke();
            return;
        }

    }
    private void MoveAllBox()
    {
        DestroyImmediate(_currentBox.gameObject);
        InitCurrentBox(_listBoxes[0]);
        foreach (var item in _listBoxes)
        {
            item.NextBox(_spacingX);
        }
    }








}
