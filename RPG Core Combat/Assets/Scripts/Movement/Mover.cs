using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IActions
    {
       [SerializeField] float maxSpeed = 6f;
        NavMeshAgent navMeshAgent;
        Health health;
        void Start()
        {
            health = GetComponent<Health>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            navMeshAgent.enabled = !health.IsDeath();
            UpdateAnim();
        }
        public void MoveToAction(Vector3 targetToMove, float speedFraction) //Move affter interact with enemies
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(targetToMove, speedFraction);
        }

        public void MoveTo(Vector3 a, float speedFraction)
        {
            navMeshAgent.destination = a;
            navMeshAgent.speed = maxSpeed*Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        void UpdateAnim() //Matching animation with movement speed
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("moveForwardSpeed", speed);
        }
        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

    }
}

