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
        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;
        Vector3 guardPos;
        float lastTimeSawPlayer = Mathf.Infinity;
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
                lastTimeSawPlayer = 0;
                AttackBehaviour();
            }
            else if (lastTimeSawPlayer < waitingTimeToBackGuard)
            {
                WaitingBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            lastTimeSawPlayer += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPos = guardPos;
            if (patrolPath != null)
            {
                if (AtWayPoint())
                {
                    CycleWayPoint();
                    Debug.Log("at a waypoint");
                }
                nextPos = GetCurrentWayPoint();

            }
            fighter.Cancel();
            mover.MoveToAction(nextPos); //Move enemy to guardPos
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
