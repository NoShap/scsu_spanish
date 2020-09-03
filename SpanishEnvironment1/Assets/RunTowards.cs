using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunTowards : MonoBehaviour
{
	private Animator anim;
	private NavMeshAgent nav;
	public Transform target;

    // Start is called before the first frame update
    void Start()
    {
       anim = GetComponent<Animator>();
    	nav = GetComponent<NavMeshAgent>();
    }


    IEnumerator runAway()
    {
    	yield return new WaitForSeconds(2f);
    	anim.SetBool("Running", true);
       	nav.destination = target.position;
    }
    // Update is called once per frame
    void Update()
    {
    	if(Input.GetKey("space")) 
    	{ 
    		StartCoroutine(runAway());
    	}
    }
}
