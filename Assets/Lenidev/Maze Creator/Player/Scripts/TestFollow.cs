using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFollow : MonoBehaviour
{
    public GameObject target;

    void Start()
    {
        
    }


    void Update()
    {
        var dir = target.transform.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position,target.transform.position, 0.05f);
        Debug.DrawRay(transform.position, dir);
    }
}
