using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
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
                    Debug.Log("NO ENEMY HERE");
                    continue;
                }  //If raycast not find an object, continue this loop
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<PlayerCombat>().Attack(targetCombat);
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
                    GetComponent<Mover>().MoveToAction(hit.point);
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

