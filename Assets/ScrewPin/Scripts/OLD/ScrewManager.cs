using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewManager : MonoBehaviour
{
    public List<CollectScrew> collectScrews = new List<CollectScrew>();

    private int currentCollectIndex = 0;
    [SerializeField] private List<GameObject> collectScrewPrefabs; // List of different CollectScrew prefabs
    //[SerializeField] private GameObject collectScrewPrefab;
    [SerializeField] private Transform spawnCollectScrewPos;
    


    private void Start()
    {
        StartCoroutine(CheckAndMoveScrews());
    }


    /*public void AddScrewToCurrentCollect(Transform screw)
    {
        if (currentCollectIndex < collectScrews.Count)
        {
            CollectScrew currentCollect = collectScrews[currentCollectIndex];
            currentCollect.AddScrew(screw);

            if (currentCollect.IsFull())
            {
                currentCollect.PushObjectAside();
                currentCollectIndex++;
            }
        }
    }*/

    private IEnumerator CheckAndMoveScrews()
    {
        for (int i = 0; i < collectScrews.Count; i++)
        {
            CollectScrew currentScrew = collectScrews[i];

            while (!currentScrew.IsFull())
            {
                yield return null;
            }

            currentScrew.PushObjectAside();

            yield return new WaitForSeconds(1.0f); // Adjust the wait time if necessary

            if (i + 1 < collectScrews.Count)
            {
                collectScrews[i + 1].transform.position = currentScrew.transform.position;
            }
        }

        Debug.Log("All screws processed.");
    }

    public void CreateNewCollectScrew()
    {
        if (collectScrewPrefabs != null && spawnCollectScrewPos != null)
        {
            // Choose a random prefab
            int randomIndex = Random.Range(0, collectScrewPrefabs.Count);
            GameObject randomCollectScrewPrefab = collectScrewPrefabs[randomIndex];

            GameObject newCollectScrewObj = Instantiate(randomCollectScrewPrefab, spawnCollectScrewPos.position, Quaternion.identity);
            CollectScrew newCollectScrew = newCollectScrewObj.GetComponent<CollectScrew>();
            if (newCollectScrew != null)
            {
                collectScrews.Add(newCollectScrew);
            }
        }
        /*if (collectScrewPrefab != null && spawnCollectScrewPos != null)
        {
            GameObject newCollectScrewObj = Instantiate(collectScrewPrefab, spawnCollectScrewPos.position, Quaternion.identity);
            CollectScrew newCollectScrew = newCollectScrewObj.GetComponent<CollectScrew>();
            if (newCollectScrew != null)
            {
                collectScrews.Add(newCollectScrew);
            }
        }*/
    }

    public void RemoveCollectScrew(CollectScrew collectScrew)
    {
        if (collectScrew != null)
        {
            collectScrews.Remove(collectScrew);
        }
    }
}