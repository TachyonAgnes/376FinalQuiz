using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// ------------------------------------------------------------------------------ 
// Quiz 
// Written by: Zisen Ling & 40020293
// For COMP 376 – Fall 2021 
// ----------------------------------------------------------------------------- 

public class BobombTrace : MonoBehaviour
{
    [SerializeField] public GameObject player;
    private NavMeshAgent nav;
    private float distance;

    void Awake()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= 5)
        {
            nav.enabled = true;
            nav.SetDestination(player.transform.position);
        }
        else
        {
            this.nav.enabled = false;
        }
    }
}
