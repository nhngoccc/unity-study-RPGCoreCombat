using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawCube(GetWayPoint(i), new Vector3(1, 1, 1));
                Gizmos.DrawLine(GetWayPoint(i), GetWayPoint(j));

            }
        }

        public int GetNextIndex(int i)
        {
            if(i+1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetWayPoint(int i)
        {
            return transform.GetChild(i).transform.position;
        }
    }
}
