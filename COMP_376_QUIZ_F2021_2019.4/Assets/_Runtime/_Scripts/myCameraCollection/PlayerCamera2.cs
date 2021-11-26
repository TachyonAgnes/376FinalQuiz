using UnityEngine;

// ------------------------------------------------------------------------------ 
// Quiz 
// Written by: Zisen Ling & 40020293
// For COMP 376 – Fall 2021 
// ----------------------------------------------------------------------------- 

// Basically merged PlayCameraWithOcclusion.cs with the given PlayerCamera.cs
// Camera Collision And occlusion In Unity | Quick Unity Tutorial |
// https://www.youtube.com/watch?v=vpn8CbPpvlU&t=14s

public class PlayerCamera2 : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField, Min(1)] private float sensitivity = 100;

    private float yaw, pitch;

    private float distanceToTarget;

    public float smoothing = 0.1f;

    private Vector3 cameraDirection;
    private Vector2 cameraDistanceMinMax = new Vector2(0.1f, 4f);
    public Transform cam;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (target != null)
        {
            cameraDirection = (transform.position - target.transform.position).normalized;
            distanceToTarget = cameraDistanceMinMax.y;

            Vector3 xzDirection = Vector3.ProjectOnPlane(cameraDirection, Vector3.up);
            pitch = Vector3.Angle(cameraDirection, xzDirection);
        }
        
    }

    private void LateUpdate()
    {
        CheckCameraOcclusionAndCollision(cam);
        yaw += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, 20, 70);
        if (target != null)
        {
            Quaternion desiredRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(pitch, yaw, 0), smoothing * Time.deltaTime);
            transform.position = target.transform.position + desiredRotation * -Vector3.forward * distanceToTarget;
            transform.LookAt(target.transform);
        }
    }

    public void CheckCameraOcclusionAndCollision(Transform _cam)
    {
        Vector3 desiredCameraPosition = transform.TransformPoint(cameraDirection * cameraDistanceMinMax.y);
        RaycastHit hit;
        if (Physics.Linecast(transform.position, desiredCameraPosition, out hit))
        {
            print(hit);
            distanceToTarget = Mathf.Clamp(hit.distance, cameraDistanceMinMax.x, cameraDistanceMinMax.y);
        }
        else
        {
            distanceToTarget = cameraDistanceMinMax.y;
        }
        _cam.localPosition = cameraDirection * distanceToTarget;
    }
}
