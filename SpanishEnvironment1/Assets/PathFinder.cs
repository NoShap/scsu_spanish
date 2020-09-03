using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFinder : MonoBehaviour
{
	private Animator anim;
	private NavMeshAgent nav;
	private bool isMoving;

	// public List<Transform> destinations;
	// transform nextTransform;
    // Start is called before the first frame update
    void Start()
    {
    	anim = GetComponent<Animator>();
    	nav = GetComponent<NavMeshAgent>();
        // nextTransform = destinations[0];
    }

    // Update is called once per frame
    void Update()
    {
    	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    	RaycastHit hit;

    	if(Input.GetMouseButtonDown(0))
    	{
    		if(Physics.Raycast(ray, out hit, 100))
    		{
    			nav.destination = hit.point;
    		}
    	}

    	if(nav.remainingDistance <= nav.stoppingDistance)
    	{
    		isMoving = false;
            // anim.Play("Idle", 0, 0f);
    	}
    	else
    	{
            print("moving");
    		isMoving = true;
            // anim.Play("Walking", 0, 0f);
    	}

    	anim.SetBool("Moving", isMoving);

    }

}
