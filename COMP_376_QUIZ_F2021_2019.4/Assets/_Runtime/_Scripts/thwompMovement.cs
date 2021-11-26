using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thwompMovement : MonoBehaviour
{
    [SerializeField] private GameObject Thwomp;
    private Vector3 originPos;
    private bool isDown; 
    private float downCounter;
    // Start is called before the first frame update
    void Start()
    {
        originPos = Thwomp.transform.position;
        isDown = false;
        downCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDown)
        {
            downCounter += Time.deltaTime;
            if (downCounter >= 3)
            {
                Thwomp.transform.position = originPos; 
                downCounter = 0;
                isDown = false;
                Thwomp.GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            Thwomp.GetComponent<Rigidbody>().useGravity = true;
            isDown = true;
        }
    }
}
