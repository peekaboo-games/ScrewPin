using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoInternetPopup : GameMonobehavior
{
    private void OnEnable()
    {
        StartCoroutine(TurnOffPopup());
    }

    private void OnDisable()
    {
        StopCoroutine(TurnOffPopup());
    }
    public IEnumerator TurnOffPopup()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
