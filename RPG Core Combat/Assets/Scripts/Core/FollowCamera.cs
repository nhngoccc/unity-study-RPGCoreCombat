using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        public Transform targetCamera;

        void LateUpdate()
        {
            transform.position = targetCamera.position;
        }
    }

}
