using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class PlayerCombat : MonoBehaviour, IActions
    {
        [SerializeField] float weaponRange = 1f;
        [SerializeField] float timeBetweenAttack = 2f;
        [SerializeField] int damage = 10;
        float timeAfterLastAttack;
        Transform target;
        private void Update()
        {
            timeAfterLastAttack += Time.deltaTime;
            if (target == null)
            {
                return;
            }
            if (!CompareDistance())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                Attacking();
            }
        }
        private bool CompareDistance() //Compare distance: Player to Enemies
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(TargetCombat targetCombat)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = targetCombat.transform;
        }
        public void Cancel()
        {
            target = null;
        }
        void Attacking()
        {
            if (timeAfterLastAttack > timeBetweenAttack)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeAfterLastAttack = 0;

            }

        }
        //Animation events
        void Hit()
        {
            target.GetComponent<Health>().TakeDamager(damage);
            Debug.Log("attacking");
        }

    }

}
