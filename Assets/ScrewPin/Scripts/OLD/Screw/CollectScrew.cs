using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class CollectScrew : MonoBehaviour
{
    public enum Collect
    {
        screw_1 = 1,
        screw_2 = 2,
        screw_3 = 3,
        screw_4 = 4,
        screw_5 = 5,
    }

    public Collect id;
    public List<Transform> positions = new List<Transform>();
    public List<Transform> screws = new List<Transform>();
    public int maxCapacity = 5;

    [SerializeField] private GameObject lidPrefab;
    [SerializeField] private Transform spawnLidPos;
    private GameObject lidInstance;

    public Transform GetFirstPosition()
    {
        return positions.Count > 0 ? positions[0] : null;
    }

    public void RemoveFirstPosition()
    {
        if (positions.Count > 0)
        {
            positions.RemoveAt(0);
        }
    }

    public bool IsFull()
    {
        return screws.Count >= maxCapacity;
    }

    public void AddScrew(Transform screw)
    {
        screws.Add(screw);
        if (IsFull())
        {
            PushObjectAside();
        }
    }

    public void PushObjectAside()
    {
        if (IsFull())
        {
            if (lidPrefab != null)
            {
                if (lidInstance == null)
                {
                    lidInstance = Instantiate(lidPrefab, spawnLidPos);
                }

                Sequence sequence = DOTween.Sequence();
                sequence.Append(lidInstance.transform.DOMove(transform.position, 0.5f).OnComplete(() =>
                {
                    lidInstance.transform.SetParent(transform);
                }));

                sequence.Append(transform.DOMove(transform.position + new Vector3(-10f, 0f, 0f), 0.5f));

                sequence.OnComplete(() =>
                {
                    // Xóa CollectScrew hiện tại và tạo mới
                    ScrewManager screwManager = FindObjectOfType<ScrewManager>();
                    if (screwManager != null)
                    {
                        screwManager.RemoveCollectScrew(this);
                        Destroy(gameObject);
                        screwManager.CreateNewCollectScrew();
                    }
                });
            }
        }
    }
}
