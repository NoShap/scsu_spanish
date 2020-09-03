using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCFollow : MonoBehaviour
{
 	
 	public GameObject ThePlayer;
 	public float TargetDistance;
 	public float AllowedDistance = 6;
 	public float FollowSpeed;
 	public RaycastHit Shot;
 	public NavMeshAgent agent;
 	private Animator m_Animator;

 	void Start()
 	{
 		m_Animator = gameObject.GetComponent<Animator>();
 	}

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(ThePlayer.transform);
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot)) 
        {
        	TargetDistance = Shot.distance;
        	if(TargetDistance >= AllowedDistance)
        	{
        		//FollowSpeed = 0.12f;
        		//transform.position = Vector3.MoveTowards(transform.position, ThePlayer.transform.position, FollowSpeed);
        		//to change agent speed, set agent.speed = ...;
        		m_Animator.SetBool("Walking", true);
        		agent.speed = 10;
        		Vector3 targetPosition = new Vector3(ThePlayer.transform.position.x + AllowedDistance, ThePlayer.transform.position.y, ThePlayer.transform.position.z);
        		agent.SetDestination(targetPosition);

        	}
        	else
        	{
        		m_Animator.SetBool("Walking", false);
        		//FollowSpeed = 0;
        	}
        }
    }
}
