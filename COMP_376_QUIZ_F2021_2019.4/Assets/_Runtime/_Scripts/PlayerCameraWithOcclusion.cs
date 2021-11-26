using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraWithOcclusion : MonoBehaviour
{
    [SerializeField, Min(1)] private float sensitivity = 100;
    private float mouseX, mouseY;
    private float smoothing = 5;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    public Transform target;

    private Vector3 cameraDirection;
    private float camDistance;
    private Vector2 cameraDistanceMinMax = new Vector2(0.5f, 5f);
    private Transform cam;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //if (target != null)
        //{
        //    cameraDirection = (transform.position - target.transform.position).normalized;
        //    camDistance = cameraDistanceMinMax.y;

        //    Vector3 xzDirection = Vector3.ProjectOnPlane(cameraDirection, Vector3.up);
        //    mouseY = Vector3.Angle(cameraDirection, xzDirection);
        //}

    }

    //private void Update()
    //{
    //    CheckCameraOcclusionAndCollision(cam);

    //}

    private void LateUpdate()
    {
            mouseX += Input.GetAxis("Mouse X") * sensitivity;
            mouseY -= Input.GetAxis("Mouse Y") * sensitivity;
            mouseY = Mathf.Clamp(mouseY, 20, 70);

        //Quaternion desiredRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(mouseY, mouseX, 0), smoothing * Time.deltaTime);
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(mouseX, mouseY), ref rotationSmoothVelocity, smoothing);
        transform.eulerAngles = currentRotation;

        transform.position = Vector3.MoveTowards(transform.position, target.position, 0.5f);
    }

    //public void CheckCameraOcclusionAndCollision(Transform _cam)
    //{
    //    Vector3 desiredCameraPosition = transform.TransformPoint(cameraDirection * cameraDistanceMinMax.y);
    //    RaycastHit hit;
    //    if (Physics.Linecast(transform.position, desiredCameraPosition, out hit))
    //    {
    //        camDistance = Mathf.Clamp(hit.distance, cameraDistanceMinMax.x, cameraDistanceMinMax.y);
    //    }
    //    else
    //    {
    //        camDistance = cameraDistanceMinMax.y;
    //    }
    //    _cam.localPosition = cameraDirection * camDistance;
    //}
}
