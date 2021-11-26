using System.Collections;
using System.Collections.Generic;

using UnityEngine;

// ------------------------------------------------------------------------------ 
// Quiz 
// Written by: Zisen Ling & 40020293
// For COMP 376 – Fall 2021 
// ----------------------------------------------------------------------------- 

// Filmstorm - Free 3rd Person Camera Setup & Camera Collision Tutorial
// https://www.youtube.com/watch?v=LbDQHv9z-F0
// works pretty good

public class CameraCollision : MonoBehaviour
{
    public float minDistance = 1.0f;
    public float maxDistance = 4.0f;
    public float smooth = 10;
    Vector3 dollyDir;
    public Vector3 dollyDirAdjusted;
    public float distance;

    private void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    void Update()
    {
        Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * maxDistance);
        RaycastHit hit;
        if(Physics.Linecast(transform.parent.position, desiredCameraPos, out hit)){
            Debug.Log(hit.collider.tag);
            distance = Mathf.Clamp(hit.distance * .9f, minDistance, maxDistance);
        }
        else{
            distance = maxDistance;
        }


        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
    }

}
