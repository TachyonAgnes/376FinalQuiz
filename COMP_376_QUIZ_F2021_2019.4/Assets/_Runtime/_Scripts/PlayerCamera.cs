using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField, Min(1)] private float sensitivity = 100;

    private float mouseX, mouseY;

    private float distanceToTarget;

    public float smoothing = 5;

    private Vector3 cameraDirection;
    private Vector2 cameraDistanceMinMax = new Vector2(1.0f, 5f);
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
            mouseY = Vector3.Angle(cameraDirection, xzDirection);


            //Vector3 offset = transform.position - target.transform.position;
            //distanceToTarget = offset.magnitude;

            //Vector3 xzDirection = Vector3.ProjectOnPlane(offset, Vector3.up);
            //mouseY = Vector3.Angle(offset, xzDirection);
        }
        
    }

    //private void Update()
    //{
    //    if (target != null)
    //    {
    //        mouseX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
    //        mouseY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
    //        mouseY = Mathf.Clamp(mouseY, 20, 70);
    //    }
    //    CheckCameraOcclusionAndCollision(cam);
    //    if (target != null)
    //    {
    //        Quaternion desiredRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(mouseY, mouseX, 0), smoothing * Time.deltaTime);

    //        transform.position = target.transform.position + desiredRotation * -Vector3.forward * distanceToTarget;
    //        transform.LookAt(target.transform);
    //    }
    //}

    private void LateUpdate()
    {
        //CheckCameraOcclusionAndCollision(cam);
        if (target != null)
        {
            mouseX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            mouseY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            mouseY = Mathf.Clamp(mouseY, 20, 70);
        }
        if (target != null)
        {
            Quaternion desiredRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(mouseY, mouseX, 0), smoothing * Time.deltaTime);

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
            distanceToTarget = Mathf.Clamp(hit.distance, 0.1f, 10.0f);
        }
        else
        {
            distanceToTarget = 10.0f;
        }
        _cam.localPosition = cameraDirection * distanceToTarget;
    }
}
