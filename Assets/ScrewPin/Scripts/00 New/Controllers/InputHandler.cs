using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputHandler : GameMonobehavior
{
    public static InputHandler Instance;

    [SerializeField] float _radius;
    [SerializeField] LayerMask _mask;
    private bool canClick = true; // Add this flag

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Gm.cantClick)
        {
            Clicked();

        }
    }
    private void Clicked()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.SphereCastAll(ray.origin, _radius, ray.direction, Mathf.Infinity, _mask);
        RaycastHit[] arrHits = hits.OrderBy(screw => screw.distance).ToArray();
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        if (arrHits.Length > 0)
        {
            ScrewItem screw = arrHits[0].collider.GetComponent<ScrewItem>();
            if (screw)
            {
                screw.MoveToTarget();
                Ac.PlaySound(Ac.screw);
            }
            else
            {
                Ac.PlaySound(Ac.notScrew);
            }
                
        }
    }
    public void SetClickability(bool state)
    {
        canClick = state;
    }
}
