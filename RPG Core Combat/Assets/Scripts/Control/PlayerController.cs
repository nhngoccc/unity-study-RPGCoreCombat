using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;
        private void Start()
        {
            health = GetComponent<Health>();
        }
        void Update()
        {
            if (health.IsDeath()) return;
            if (CombatInteract()) return;
            if (MoveInteract()) return;
        }
        private bool CombatInteract() //Move player to enemies to combat
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                TargetCombat targetCombat = hit.transform.GetComponent<TargetCombat>();
                if (targetCombat == null)
                {
                    continue;
                }  //If raycast not find an object, continue this loop
                if (!GetComponent<Fighter>().CanAttack(targetCombat.gameObject)) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(targetCombat.gameObject);
                }
                return true;
            }
            return false;
        }



        private bool MoveInteract() //Move player to what raycast point is
        {
            RaycastHit hit; //get information about what raycast hit (positon)
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0)) // Move to what mouse point to and cancel combat status
                {
                    GetComponent<Mover>().MoveToAction(hit.point,1f);
                }
                return true;
            }
            return false;


        }

        private static Ray GetMouseRay() //Get raycast point
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
            // Debug.DrawRay(GetMouseRay().origin, GetMouseRay().direction * 100, Color.red);

        }
    }
}

