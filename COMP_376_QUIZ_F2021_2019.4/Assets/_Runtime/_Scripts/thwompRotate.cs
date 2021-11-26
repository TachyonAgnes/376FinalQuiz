using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ------------------------------------------------------------------------------ 
// Quiz 
// Written by: Zisen Ling & 40020293
// For COMP 376 – Fall 2021 
// ----------------------------------------------------------------------------- 

public class thwompRotate : MonoBehaviour
{
    [SerializeField] public GameObject player;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= 10)
        {
            var lookPos = player.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 0.6f);
        }
    }
}
