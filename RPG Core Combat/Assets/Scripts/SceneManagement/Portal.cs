using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.AI;
namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad;
        [SerializeField] Transform spawnPoint;
        [SerializeField] PortalDestination portalDestination;
        [SerializeField] float timeToFadeOut = 2f;
        [SerializeField] float timeToFadeIn = 2f;
        [SerializeField] float timeWaitToFadeInOut = 2f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(Transition());
            }
            Debug.Log("Protal trigger");
        }
        private IEnumerator Transition()
        {
            Fader fader = FindObjectOfType<Fader>();
            DontDestroyOnLoad(gameObject);

            //VFX
            yield return fader.FadeOut(timeToFadeOut); //fade out effect

            //
            yield return SceneManager.LoadSceneAsync(sceneToLoad);


            Portal otherPortal = GetOtherPortal();
            UpdatePlayerPos(otherPortal);

            //VFX
            yield return new WaitForSeconds(timeWaitToFadeInOut); //waiting after fade out
            yield return fader.FadeIn(timeToFadeIn); //fade in effect

            Destroy(gameObject);

            Debug.Log("Load portal scene ");

        }

        private void UpdatePlayerPos(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.portalDestination != portalDestination) continue;
                return portal;
            }
            return null;
        }
        enum PortalDestination
        {
            A, B, C, D, E, F
        }
    }

}
