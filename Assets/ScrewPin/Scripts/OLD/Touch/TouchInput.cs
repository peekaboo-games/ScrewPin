using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Click;

public class TouchInput : MonoBehaviour
{
    RaycastDetector raycastDetector = new();
    [SerializeField] private UserInput _userInput;
    [SerializeField] private int _nutLayer;


    private void Start()
    {
        _userInput.OnMouseDown += OnMouseDownHandler;
        _userInput.OnMouseMove += OnMouseMoveHandler;
        _userInput.OnMouseUp += OnMouseUpHandler;
    }

    private void OnMouseUpHandler()
    {

    }

    private void OnMouseMoveHandler()
    {
       
    }

    private void OnMouseDownHandler()
    {
        ContactInfo contactInfo = raycastDetector.RayCastCross(/*_nutLayer*/);

        if (contactInfo.contacted)
        {

            Debug.Log(contactInfo.transform.tag);
            if (contactInfo.transform.tag == "Board") return;
            bool isNut = contactInfo.collider.TryGetComponent<Nut>(out Nut nut);
            if(isNut)
            {
                nut.MoveObj();
            }
        }
    }
}
