using UnityEngine;
using System;

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
    public GameManager gameManager;

    private GameObject placeObj;
    private GameObject placeObjOutline;

    public GameObject terra;
    public GameObject terra_outline;

    public GameObject tomate;
    public GameObject tomate_outline;

    private Vector3 offset = new Vector3(0.5f, 0.22f, 0.5f);


    void Update()
    {
        if (cam == null) return;
        if (placeObj == null || placeObjOutline == null) return;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            hitPoint = hit.point;
            distanceToGround = hit.distance;
            hitCollider = hit.collider;

            double x = Math.Ceiling(hitPoint.x);
            x = x - offset.x;
            double z = Math.Ceiling(hitPoint.z);
            z = z - offset.z;
            double y = offset.y;

            Vector3 pos = new Vector3((float)x, (float) y, (float) z);
            gridPoint = pos;
            placeObjOutline.transform.position = pos;
        }
        else
        {
            distanceToGround = maxDistance;
            hitPoint = ray.origin + ray.direction * maxDistance;
            hitCollider = null;
        }

        if(gridPoint == null)
        {
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(placeObj, gridPoint, Quaternion.identity);
        }
    }

    private void destroyOutlineObjs()
    {
        Destroy(placeObjOutline);
        placeObjOutline = null;
    }

    public void setGroundObj()
    {
        destroyOutlineObjs();
        placeObj = terra;
        placeObjOutline = Instantiate(terra_outline, new Vector3(0f, -100f, 0f), Quaternion.identity);
        offset = new Vector3(0.5f, 0.22f, 0.5f);
    }

    public void setTomateObj()
    {
        destroyOutlineObjs();
        placeObj = tomate;
        placeObjOutline = Instantiate(tomate_outline, new Vector3(0f, -100f, 0f), Quaternion.identity);
        offset = new Vector3(0.35f, 0.4f, 0.45f);
    }

    public void resetObj()
    {
        destroyOutlineObjs();
        placeObj = null;
        placeObjOutline = null;
    }
}
