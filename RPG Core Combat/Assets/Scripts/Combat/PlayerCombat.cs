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
        Transform target;
        private void Update()
        {
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
            Debug.Log("Attack the enemiesss!!!!!");
        }
        public void Cancel()
        {
            target = null;
        }

    }
}
