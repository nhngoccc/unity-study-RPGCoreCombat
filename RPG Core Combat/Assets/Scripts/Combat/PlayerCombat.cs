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
        Health target;
        public bool CanAttack(TargetCombat targetCombat)
        {
            Health targetToAttack = targetCombat.GetComponent<Health>();
            return targetToAttack != null  && !targetToAttack.IsDeath();

        }
        private void Update()
        {
            timeAfterLastAttack += Time.deltaTime;
            if (target == null)
            {
                return;
            }

            if (target.IsDeath()) return;

            if (!CompareDistance())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                Attacking();
            }
        }
        private bool CompareDistance() //Compare distance: Player to Enemies
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(TargetCombat targetCombat)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = targetCombat.GetComponent<Health>();
        }
        public void Cancel()
        {
            StopAnim();
            target = null;
        }

        private void StopAnim()
        {
            GetComponent<Animator>().ResetTrigger("cancelAnimation");
            GetComponent<Animator>().SetTrigger("cancelAnimation");
        }

        void Attacking()
        {
            transform.LookAt(target.transform);
            if (timeAfterLastAttack > timeBetweenAttack)
            {
                StartAttack();
                timeAfterLastAttack = 0;

            }

        }

        private void StartAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        //Animation events
        void Hit()
        {
            target.TakeDamager(damage);
            Debug.Log("attacking");
        }

    }

}
