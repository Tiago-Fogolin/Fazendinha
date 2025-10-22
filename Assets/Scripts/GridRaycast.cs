using UnityEngine;
using System;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;


public class GridRaycast : MonoBehaviour
{
    public float maxDistance = 100f;

    [HideInInspector]
    public Vector3 hitPoint;
    [HideInInspector]
    public float distanceToGround;
    [HideInInspector]
    public Collider hitCollider;
    [HideInInspector]
    public Vector3 gridPoint;

    public Camera cam;

    public bool hasHit = false;

    void Update()
    {
        if (cam == null) return;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            hasHit = true;
            hitPoint = hit.point;
            distanceToGround = hit.distance;
            hitCollider = hit.collider;
        }
        else
        {
            distanceToGround = maxDistance;
            hitPoint = ray.origin + ray.direction * maxDistance;
            hitCollider = null;
            hasHit = false;
        }

    }

}
