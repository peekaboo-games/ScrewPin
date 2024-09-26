using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarItem : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Limit"))
        {
            gameObject.SetActive(false);
        }
    }
}
