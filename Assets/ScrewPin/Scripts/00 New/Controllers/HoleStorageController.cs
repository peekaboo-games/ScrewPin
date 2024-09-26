using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HoleStorageController : GameMonobehavior
{
    [SerializeField] HoleStorageItem _holeStorageItemPrefab;
    [SerializeField] List<HoleStorageItem> _holes;
    [SerializeField] float _spacingX;
    public List<HoleStorageItem> Holes => _holes;
    public List<int> holeHasScrews = new List<int>();
    public List<MaskObject> masks;
    


    private void Awake()
    {
        Gm.HoleStorageController = this;
    }

    private void Start()
    {
        Gpm.OnAddHole += AddNewHole;
    }
    [Button("Fitter Horizontal")]
    private void AutoFitterHorizontal()
    {
        float posX = 0;
        foreach (var item in _holes)
        {
            item.transform.localPosition = Vector3.right * posX;
            posX += _spacingX;
        }
        
    }

    public void ResetHoleHasScrews(int indexHole)
    {
        if (holeHasScrews.Contains(indexHole))
        {
            Debug.LogWarning("Reset");
            holeHasScrews.Remove(indexHole);
            masks.Remove(masks[indexHole]);
        }
    }

    public Transform GetHoleStorageEmpty(ScrewItem screw)
    {
        if (screw.isHole) return null;
        screw.isHole = true;
        foreach (var item in _holes)
        {
            if (!item.IsHasScrew)
            {
                item.IsHasScrew = true;
                item.SetScrewItem(screw);
                if (CheckLose())
                {
                    Observe.OnLose?.Invoke();
                    
                    Gm.cantClick = true;
                }
                Debug.Log(item.transform);
                
                return item.transform;
            }
        }

        return null;
    }

    private bool CheckLose()
    {
        foreach (var item in Holes)
        {
            if (!item.IsHasScrew)
                return false;
        }
        
        return true;
    }
    
    [Button("Add Hole")]
    public void AddNewHole()
    {
        
        var newHole = Instantiate(_holeStorageItemPrefab, transform);
       
        _holes.Add(newHole);
        if(_holes.Count  > 5)
        {
            gameObject.transform.DOMoveX(-2.5f, 0.25f);
        }
        if(_holes.Count == 7)
        {
            gameObject.transform.DOMoveX(-3f, 0.25f);
        }
        AutoFitterHorizontal();
    }

   
    private void OnDestroy()
    {
        Gpm.OnAddHole -= AddNewHole;

    }


}
