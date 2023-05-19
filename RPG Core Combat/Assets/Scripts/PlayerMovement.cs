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
        UpdateAnim();
    }
    //Move player to mouse point method
    void MoveToCursor()
    {
        if (Input.GetMouseButton(0))
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
    void UpdateAnim()
    {
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("moveForwardSpeed", speed);
    }
}
