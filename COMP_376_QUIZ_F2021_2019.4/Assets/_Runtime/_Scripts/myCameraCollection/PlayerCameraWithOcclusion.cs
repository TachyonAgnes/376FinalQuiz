using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ------------------------------------------------------------------------------ 
// Quiz 
// Written by: Zisen Ling & 40020293
// For COMP 376 – Fall 2021 
// ----------------------------------------------------------------------------- 

// https://www.youtube.com/watch?v=vpn8CbPpvlU&t=14s
// Camera Collision And occlusion In Unity | Quick Unity Tutorial |
// halfly Working, with wasd problem + most of time camera collision won't work


public class PlayerCameraWithOcclusion : MonoBehaviour
{
    public float mouseSensitivity = 10f;
    public Transform target;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    private float smoothing = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    private float yaw, pitch;

    Vector3 cameraDirection;
    float camDistance;
    Vector2 cameraDistanceMinMax = new Vector2(0.5f, 5f);
    public Transform cam;

    private void Awake()
    {
        cameraDirection = cam.transform.localPosition.normalized;
        camDistance = cameraDistanceMinMax.y;

        //Vector3 xzDirection = Vector3.ProjectOnPlane(cameraDirection, Vector3.up);
        //mouseY = Vector3.Angle(cameraDirection, xzDirection);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        CheckCameraOcclusionAndCollision(cam);
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
       pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
       pitch = Mathf.Clamp(pitch, 20, 70);

       currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch,yaw), ref rotationSmoothVelocity, smoothing);
       transform.eulerAngles = currentRotation;
       transform.position = Vector3.MoveTowards(transform.position, target.position, 0.5f);
    }

    public void CheckCameraOcclusionAndCollision(Transform _cam)
    {
        Vector3 desiredCameraPosition = transform.TransformPoint(cameraDirection * cameraDistanceMinMax.y);
        RaycastHit hit;
        if (Physics.Linecast(transform.position, desiredCameraPosition, out hit))
        {
            camDistance = Mathf.Clamp(hit.distance, cameraDistanceMinMax.x, cameraDistanceMinMax.y);
        }
        else
        {
            camDistance = cameraDistanceMinMax.y;
        }
        _cam.localPosition = cameraDirection * camDistance;
    }
}
