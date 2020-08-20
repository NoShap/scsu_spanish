using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DestinationMove : MonoBehaviour
{
	private Animator anim;
	private NavMeshAgent nav;
	private bool isMoving;

	public List<Transform> destinations;
	Transform nextTransform;
    // Start is called before the first frame update
    void Start()
    {
    	anim = GetComponent<Animator>();
    	nav = GetComponent<NavMeshAgent>();
    	nav.destination = destinations[0].position;
    	destinations.RemoveAt(0);

        // nextTransform = destinations[0];
    }

    // Update is called once per frame
    void Update()
    {

    	if(nav.remainingDistance <= nav.stoppingDistance)
    	{
    		if(destinations.Count != 0)
    		{
    			nav.destination = destinations[0].position;
    			destinations.RemoveAt(0);
    		}
    		else
    		{
    			isMoving = false;
    		}
    		
    	}
    	else
    	{
    		isMoving = true;
    	}

    	anim.SetBool("Moving", isMoving);

    }

    void walkForward()
    {

    }
}
