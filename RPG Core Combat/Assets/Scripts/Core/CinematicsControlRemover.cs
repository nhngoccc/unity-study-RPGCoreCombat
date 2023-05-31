using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Control;
using RPG.Core;
namespace RPG.Cinematics
{
    public class CinematicsControlRemover : MonoBehaviour
    {
        GameObject player;
        private void Start() {
            GetComponent<PlayableDirector>().played += DisableControl; //Disable control when playabledirector called
            GetComponent<PlayableDirector>().stopped += EnableControl;
            player = GameObject.FindWithTag("Player");
        }
        void DisableControl(PlayableDirector playableDirector)
        {
            Debug.Log("Disable Control");
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }
        void EnableControl(PlayableDirector playableDirector)
        {
            Debug.Log("Enable Control");
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}

