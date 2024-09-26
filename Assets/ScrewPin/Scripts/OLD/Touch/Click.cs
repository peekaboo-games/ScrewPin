using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public struct ContactInfo
    {
        public bool contacted;
        public Vector3 point;
        public Collider collider;
        public Transform transform;
    }

    public class RaycastDetector
    {
        /*public ContactInfo RayCastCross(int layerMask)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            bool hit = Physics.Raycast(ray, out RaycastHit hitInfo, float.PositiveInfinity, 1 << layerMask);
            return new ContactInfo
            {
                contacted = hit,
                point = hitInfo.point,
                collider = hitInfo.collider,
                transform = hitInfo.transform
            };
        }*/
        public ContactInfo RayCastCross()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            int layerMask = ~((1 << 0) | (1 << 1) | (1 << 2) | (1 << 3) | (1 << 4) | (1 << 5));

            bool hit = Physics.Raycast(ray, out RaycastHit hitInfo, float.PositiveInfinity, layerMask);
            return new ContactInfo
            {
                contacted = hit,
                point = hitInfo.point,
                collider = hitInfo.collider,
                transform = hitInfo.transform
            };
        }

        public Vector3 RayCastTest()
        {
            Vector3 r2 = Camera.main.ViewportToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
            return r2;
        }
    }
}
