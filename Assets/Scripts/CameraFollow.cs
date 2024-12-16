using UnityEngine;
using System.Collections.Generic;
using Unity.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float FollowSpeed = 0.125f;
    

    void Update()
    {

        Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed*Time.deltaTime);
    }
}
