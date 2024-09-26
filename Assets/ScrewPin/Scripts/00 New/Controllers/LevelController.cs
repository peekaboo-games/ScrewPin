using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Linq;

public class LevelController : GameMonobehavior
{
    [SerializeField] ScrewController _screwController;
    [SerializeField] BoxController _boxController;
    [SerializeField] List<TypeColor> _typeColors;
    [SerializeField] private List<ScrewItem> screws = new List<ScrewItem>();



    int quantityBox = 0;

    private void Start()
    {
        Gpm.OnShuffle += NewShuffle;
        Gpm.OnAutoMatch += AutoMatch;
        GenScrew();
    }

    private bool Check()
    {
        Gm.HoleStorageController.masks = FindObjectsOfType<MaskObject>().ToList();
        quantityBox = FindObjectsOfType<BoxItem>().Length;

        if (Gm.HoleStorageController.masks.Count != (quantityBox * 3) || _typeColors.Count < 1)
        {
            Debug.LogError("so luong oc va so luong khay khong bang nhau");
            return false;
        }
        return true;
    }
    [Button]
    private void GenScrew()
    {
        if (!Check()) return;
        int rand = 0;
        List<TypeColor> _listColorCache = new();
        for (int i = 0; i < _typeColors.Count; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _listColorCache.Add(_typeColors[i]);
            }
        }
        Shuffle(_listColorCache);
        for (int i = 0; i < Gm.HoleStorageController.masks.Count; i++)
        {
            rand = Random.Range(0, _typeColors.Count);
            ScrewItem screw = Instantiate(_screwController.DataSOScrew.GetScrewItemOfType(_listColorCache[i]));
            screw.transform.SetParent(transform);
            if (screw != null)
            {
                Gm.HoleStorageController.holeHasScrews.Add(i);
                screw.indexHole = i;
                screw.transform.position = Gm.HoleStorageController.masks[i].transform.position;
                screw.HingerJoint.connectedBody = Gm.HoleStorageController.masks[i].GetComponentInParent<BarItem>().GetComponent<Rigidbody>();
                // sup 3
                screws.Add(screw);

            }
        }
    }


    private static System.Random rng = new System.Random();
    public static void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public void NewShuffle()
    {
        /*if (screws.Count == 0) return;

        List<Transform> positions = new List<Transform>();
        if(positions.Count > 0) { positions.Clear(); }
        foreach (var mask in Gm.HoleStorageController.masks)
        {
            positions.Add(mask.transform);
        }
        Debug.Log(positions.Count);
        Shuffle(positions);

        for (int i = 0; i < screws.Count; i++)
        {
            if (!screws[i].isHole)
            {
                //if (i >= positions.Count) return;
                Debug.Log("Hole: "+ positions[i].name);
                screws[i].transform.position = positions[i].position;
                MaskObject newMask = positions[i].GetComponent<MaskObject>();
                if(screws[i].HingerJoint != null)
                {
                    screws[i].HingerJoint.connectedBody = null;
                    screws[i].HingerJoint.connectedBody = newMask.GetComponentInParent<BarItem>().GetComponent<Rigidbody>();
                }
            }
        }*/
        if (screws.Count == 0) return;

        List<Transform> positions = new List<Transform>();
        foreach (var mask in Gm.HoleStorageController.masks)
        {
            positions.Add(mask.transform);
        }

        Shuffle(positions);
        for (int i = 0; i < screws.Count; i++)
        {
            if (!screws[i].isHole)
            {
                screws[i].transform.position = positions[i].position;
                MaskObject newMask = positions[i].GetComponent<MaskObject>();

                if (newMask != null)
                {
                    BarItem barItem = newMask.GetComponentInParent<BarItem>();
                    if (barItem != null)
                    {
                        if (screws[i].HingerJoint != null)
                        {
                            screws[i].HingerJoint.connectedBody = null;
                            screws[i].HingerJoint.connectedBody = barItem.GetComponent<Rigidbody>();
                        }
                    }
                    else
                    {
                        Debug.LogError("BarItem component not found in parent of newMask.");
                    }
                }
                else
                {
                    Debug.LogError("MaskObject component not found on position transform.");
                }
            }
        }

        /*for (int i = 0; i < screws.Count; i++)
        {
            if (!screws[i].isHole)
            {
                screws[i].transform.position = positions[i].position;
                MaskObject newMask = positions[i].GetComponent<MaskObject>();
                if (screws[i].HingerJoint != null)
                {
                    screws[i].HingerJoint.connectedBody = null;
                    screws[i].HingerJoint.connectedBody = newMask.GetComponentInParent<BarItem>().GetComponent<Rigidbody>();
                }
            }
        }*/



    }

    public void AutoMatch()
    {
        foreach (var item in screws)
        {
            if (item.TypeColor == Gm.BoxController.CurrentBox.TypeColor)
            {
                if (!item.isHole)
                {
                    item.MoveToTarget(isBreakLimitBox: true);
                }
            }
        }
    }
    private void OnDestroy()
    {
        Gpm.OnShuffle -= NewShuffle;
        Gpm.OnAutoMatch -= AutoMatch;

    }
}
