using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ------------------------------------------------------------------------------ 
// Quiz 
// Written by: Zisen Ling & 40020293
// For COMP 376 – Fall 2021 
// ----------------------------------------------------------------------------- 

public class particalRun : MonoBehaviour
{
    private ParticleSystem ps;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ps.Play();
        }
    }
}
