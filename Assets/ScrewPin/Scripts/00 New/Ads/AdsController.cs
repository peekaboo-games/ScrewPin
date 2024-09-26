using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsController : MonoBehaviour
{
    public float interAfkTime = 30f;

    private void Start()
    {
        AdManager.instance.ShowBanner();
        //StartCoroutine(ShowInterAfkEvery30Seconds());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ResetTime();
        }
    }
    /*private IEnumerator ShowInterAfkEvery30Seconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(interAfkTime);
            InterAfk();
        }
    }*/

    public void ResetTime()
    {
        interAfkTime = 30f;
    }
    /*public void InterAfk()
    {
        AdManager.instance.ShowInter(() =>
        {
            ResetTime();
        },
        ()=>{
            ResetTime();
        },"Null");
    }*/

    /*public void Inter()
    {
        AdManager.instance.ShowInter(() =>
        {

        },
        () => {

        }, "Null");
    }*/




}
