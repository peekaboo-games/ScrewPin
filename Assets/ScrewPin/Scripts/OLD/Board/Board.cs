using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public List<GameObject> screws = new List<GameObject>();



    public int boardID;
    // [SerializeField] private List<GameObject> screwPrefab; 
    

    [SerializeField] Transform[] _pointsNut;
    [SerializeField] GameObject _screwPrefab;
    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {

        foreach (var item in _pointsNut)
        {
            /*int randomIndex = Random.Range(0, _screwPrefabs.Count);
            GameObject randomScrewPrefab = _screwPrefabs[randomIndex];
            GameObject objClone = Instantiate(randomScrewPrefab, item.transform.position, Quaternion.identity);

            screws.Add(objClone);

            // Check if the screw's ID matches the board's ID
            ScrewInfo screwInfo = objClone.GetComponent<ScrewInfo>();
            if (screwInfo != null && (int)screwInfo.id == boardID)
            {
                CheckSingleScrew();
            }*/


            /*int randomIndex = Random.Range(0, screwPrefab.Count);
            GameObject randomScrewPrefab = screwPrefab[randomIndex];  
            GameObject newScrewPrefabObj = Instantiate(randomScrewPrefab, item.transform.position, Quaternion.identity);
            Nut newNut = newScrewPrefabObj.GetComponent<Nut>();
            ScrewInfo newScrewinfo = newScrewPrefabObj.GetComponent<ScrewInfo>();
            if(newNut != null && newScrewinfo != null)
            {
                screws.Add(newScrewPrefabObj);
                
            }*/



            GameObject objClone = Instantiate(_screwPrefab);

            objClone.transform.position = item.transform.position;
            screws.Add(objClone);

            /* HingeJoint hin = gameObject.AddComponent<HingeJoint>();
             hin.anchor = item.transform.position;
             hin.axis = new Vector3(0, 0, 1);*/



        }
    }



    public void CheckSingleScrew()
    {
        if (screws.Count == 1)
        {

            Transform childTransform = screws[0].transform.GetChild(0);


            transform.SetParent(childTransform);
        }
        else if (screws.Count == 0)
        {
            // Gỡ đối tượng hiện tại khỏi cha của nó
            transform.SetParent(null);

            if (GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = gameObject.AddComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeRotationX |
                                 RigidbodyConstraints.FreezeRotationY;
            }


        }
    }

    public void AddScrew(GameObject screw)
    {
        screws.Add(screw);
        CheckSingleScrew();
    }


    public void RemoveScrew(GameObject screw)
    {
        screws.Remove(screw);
        CheckSingleScrew();
    }

    [Button("Increase Axis Z")]
    public void IncreaseAxisZ(float z = 0.2f)
    {
        float axisZ = transform.localPosition.z + z;
        transform.localPosition = new Vector3(transform.localPosition.x,
            transform.localPosition.y, axisZ);
    }

    [Button("Deduct Axis Z")]
    public void DeductAxisZ(float z = 0.2f)
    {
        float axisZ = transform.localPosition.z - z;
        transform.localPosition = new Vector3(transform.localPosition.x,
            transform.localPosition.y, axisZ);
    }
}






