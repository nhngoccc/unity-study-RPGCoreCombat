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
            if (CombatInteract())
            {
                Debug.DrawRay(GetMouseRay().origin, GetMouseRay().direction, Color.black);
                return;
            }
            MoveInteract();

        }
        private bool CombatInteract()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                TargetCombat targetCombat = hit.transform.GetComponent<TargetCombat>();
                if (targetCombat == null) continue;
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<PlayerCombat>().Attack(targetCombat);
                }
                return true;
            }
            return false;
        }

        private void MoveInteract()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        void MoveToCursor()
        {
            RaycastHit hit; //get information about what raycast hit (positon)
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
            Debug.DrawRay(GetMouseRay().origin, GetMouseRay().direction * 100, Color.red);
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

