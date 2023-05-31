using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
namespace RPG.Cinematics
{
    public class CinematicsController : MonoBehaviour
    {
        bool isCinemachineTrigger = false;
        private void OnTriggerEnter(Collider other)
        {
            if(!isCinemachineTrigger)
            {
                GetComponent<PlayableDirector>().Play();
                isCinemachineTrigger = true;
            }

        }

    }
}

