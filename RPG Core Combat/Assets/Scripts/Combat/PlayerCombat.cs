using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.Movement;
using RPG.Control;

namespace RPG.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] float weaponRange = 1f;
        Transform target;
        private void Update()
        {
            bool isInRange = CompareDistance();

            if (target != null && !isInRange)
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().StopMoving();

            }
        }

        private bool CompareDistance() //Compare distance: Player to Enemies
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(TargetCombat targetCombat)
        {
            target = targetCombat.transform;
            Debug.Log("Attack the enemiesss!!!!!");
        }
        public void CancelTarget()
        {
            target = null;
        }


    }
}
