using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// ------------------------------------------------------------------------------ 
// Quiz 
// Written by: Zisen Ling & 40020293
// For COMP 376 – Fall 2021 
// ----------------------------------------------------------------------------- 

public class BooTrace : MonoBehaviour
{
    [SerializeField] public GameObject Flag;
    private NavMeshAgent nav;

    void Awake()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (dialog.isTalkDone)
        {
            GetComponent<SphereCollider>().isTrigger = true;
            nav.enabled = true;
            nav.SetDestination(Flag.transform.position);
        }
        else
        {
            GetComponent<SphereCollider>().isTrigger = false;
            nav.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Flag")
        {
            HealthBar.current = 0;
        }
    }
}
