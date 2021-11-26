using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
