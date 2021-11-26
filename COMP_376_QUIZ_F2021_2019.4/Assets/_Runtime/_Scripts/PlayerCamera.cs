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

public class PlayerCamera: MonoBehaviour
{ public float CameraMoveSpeed = 120.0f;
    public GameObject CameraFollowObj;
    public float clampAngleMin = -20;
    public float clampAngleMax = 60;
    public float inputSensitivity = 150;
    public float mouseX;
    public float mouseY;

    private float rotY = 0;
    private float rotX = 0;

    private void Start(){
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update(){
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        rotY += mouseX * inputSensitivity * Time.deltaTime;
        rotX -= mouseY * inputSensitivity * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, clampAngleMin, clampAngleMax);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }

    private void LateUpdate(){
        CameraUpdater();
    }

    private void CameraUpdater(){
        Transform target = CameraFollowObj.transform;
        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

}