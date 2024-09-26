using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserInput : MonoBehaviour
{
    public UnityAction OnMouseDown;
    public UnityAction OnMouseMove;
    public UnityAction OnMouseUp;

    private bool isMouseDown;

    private void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            OnMouseDown?.Invoke();
        }
        if (isMouseDown)
        {
            OnMouseMove?.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            OnMouseUp?.Invoke();
        }*/
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isMouseDown = true;
                OnMouseDown?.Invoke();
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (isMouseDown)
                {
                    OnMouseMove?.Invoke();
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isMouseDown = false;
                OnMouseUp?.Invoke();
            }
        }
    }

}
