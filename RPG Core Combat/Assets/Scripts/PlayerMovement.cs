using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    NavMeshAgent navMeshAgent;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        MoveToCursor();
    }
    //Move player to mouse point method
    void MoveToCursor()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; //get information about what raycast hit (positon)
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                navMeshAgent.destination = hit.point;
            }
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        }


    }

}
