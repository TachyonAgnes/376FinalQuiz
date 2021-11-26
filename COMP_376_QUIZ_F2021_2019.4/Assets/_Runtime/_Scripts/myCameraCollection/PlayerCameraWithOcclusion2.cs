using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ------------------------------------------------------------------------------ 
// Quiz 
// Written by: Zisen Ling & 40020293
// For COMP 376 – Fall 2021 
// ----------------------------------------------------------------------------- 

// https://www.youtube.com/watch?v=mZXYSihSl3I
// Unity Third Person Camera Collision Detection- Unity Tutorials #30
// Working, with wasd problems...


public class PlayerCameraWithOcclusion2 : MonoBehaviour
{
    public float cameraSmoothingFactor = 1;
    public Vector2 pitchMinMax = new Vector2(-60, 60); 
    public Transform t_camera;

    private Quaternion camRotation;
    private RaycastHit hit;
    private Vector3 camera_offset;

    private void Start()
    {
        camRotation = transform.localRotation;
        camera_offset = t_camera.localPosition;
        //Cursor;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        camRotation.x += Input.GetAxis("Mouse Y") * cameraSmoothingFactor * (-1);
        camRotation.y += Input.GetAxis("Mouse X") * cameraSmoothingFactor;
        camRotation.x = Mathf.Clamp(camRotation.x, pitchMinMax.x, pitchMinMax.y);

        transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);

        if(Physics.Linecast(transform.position,transform.position + transform.localRotation * camera_offset, out hit))
        {
            t_camera.localPosition = new Vector3(0, 0, -Vector3.Distance(transform.position, hit.point));
        }
        else
        {
            t_camera.localPosition = Vector3.Lerp(t_camera.localPosition, camera_offset, Time.deltaTime);
        }
    }
}
