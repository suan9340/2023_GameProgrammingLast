using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    public float arrowSpeed;
    public Transform currentLocation;
    public Transform target;

    void Update()
    {
        Vector3 dir = target.transform.position - transform.position; //dir.y = 0f;

        Quaternion rot = Quaternion.LookRotation(dir.normalized);

        transform.rotation = rot;
    }

}
