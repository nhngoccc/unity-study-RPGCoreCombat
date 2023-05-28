using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 1f;
        [SerializeField] float waitingTimeToBackGuard = 5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float wayPointTolerance;
        [SerializeField] float timeBetweenWayPoints = 2f;
        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;
        Vector3 guardPos;
        float lastTimeSawPlayer = Mathf.Infinity;
        float lastTimeMovedToWayPoint = Mathf.Infinity;
        int currenWaypointIndex = 0;


        private void Start()
        {
            guardPos = transform.position;
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
        }
        private void Update()
        {
            if (health.IsDeath()) return;
            if (IsInDistanceToPlayer() && fighter.CanAttack(player))
            {
                AttackBehaviour();
                Debug.Log("ATTACKBEHAVIOUR WAS CALLED");
            }
            else if (lastTimeSawPlayer < waitingTimeToBackGuard)
            {
                WaitingBehaviour();
                Debug.Log("WAITINGBEHAVIOUR WAS CALLED");

            }
            else
            {
                PatrolBehaviour();
                Debug.Log("PATROLBEHAVIOUR WAS CALLED");
            }
            TimerProcess();
        }

        private void TimerProcess() //Increase time with deltaTime
        {
            lastTimeSawPlayer += Time.deltaTime;
            lastTimeMovedToWayPoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPos = guardPos;
            if (patrolPath != null)
            {
                if (AtWayPoint())
                {
                    lastTimeMovedToWayPoint = 0;
                    CycleWayPoint();
                    Debug.Log("at a waypoint");
                }
                nextPos = GetCurrentWayPoint();

            }
            fighter.Cancel();
            if (lastTimeMovedToWayPoint > timeBetweenWayPoints)
            {
                mover.MoveToAction(nextPos); //Move enemy to guardPos
            }
        }

        private Vector3 GetCurrentWayPoint() //Get near waypoint
        {
            return patrolPath.GetWayPoint(currenWaypointIndex);
        }

        private void CycleWayPoint()
        {
            currenWaypointIndex = patrolPath.GetNextIndex(currenWaypointIndex);
            Debug.Log(currenWaypointIndex);
        }
        private bool AtWayPoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWayPoint < wayPointTolerance;

        }

        private void WaitingBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            lastTimeSawPlayer = 0;
            fighter.Attack(player);
            lastTimeSawPlayer = 0f;
        }

        private bool IsInDistanceToPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;

        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
