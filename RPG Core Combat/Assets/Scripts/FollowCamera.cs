using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform targetCamera;

    void LateUpdate()
    {
        transform.position = targetCamera.position;
    }
}
