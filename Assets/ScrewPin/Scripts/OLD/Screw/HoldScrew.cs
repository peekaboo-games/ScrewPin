using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldScrew : MonoBehaviour
{
    public enum Collect
    {
        screw_1 = 1,
        screw_2 = 2,
        screw_3 = 3,
    }

    public List<Transform> positions = new List<Transform>();

    public List<Transform> screws = new List<Transform>();
    public int maxCapacity = 5;




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
            Debug.Log("lose");

        }
    }

    public void MoveScrewsToCollect(CollectScrew[] collectScrews)
    {

        foreach (var screw in screws)
        {
            ScrewInfo screwInfo = screw.GetComponent<ScrewInfo>();
            foreach (var collectScrew in collectScrews)
            {
                if ((int)collectScrew.id == (int)screwInfo.id)
                {
                    Transform targetPosition = collectScrew.GetFirstPosition();
                    if (targetPosition != null)
                    {
                        screw.position = targetPosition.position;
                        screw.SetParent(collectScrew.transform);
                        collectScrew.AddScrew(screw);
                        collectScrew.RemoveFirstPosition();
                        break;
                    }
                }
            }
        }
        screws.Clear();
    }
}
