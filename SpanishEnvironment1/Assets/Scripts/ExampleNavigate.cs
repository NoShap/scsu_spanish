using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExampleNavigate : MonoBehaviour
{
	public GameObject target;


	private UnityEngine.AI.NavMeshAgent navComponent;
	private MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
    	mesh = GetComponent<MeshRenderer>();
        navComponent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navComponent.destination = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
