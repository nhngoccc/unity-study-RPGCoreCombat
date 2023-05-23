using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Transform target;
        NavMeshAgent navMeshAgent;
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            UpdateAnim();
        }
        public void MoveToAction(Vector3 targetToMove) //Move affter interact with enemies
        {
            GetComponent<PlayerCombat>().CancelTarget();
            MoveTo(targetToMove);
        }

        public void MoveTo(Vector3 a)
        {
            navMeshAgent.destination = a;
            navMeshAgent.isStopped = false;
        }

        void UpdateAnim() //Matching animation with movement speed
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("moveForwardSpeed", speed);
        }
        public void StopMoving()
        {
            navMeshAgent.isStopped = true;
        }
    }
}

