using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DestinationMove : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent nav;
    private bool isMoving;
    AnimatorClipInfo[] m_CurrentClipInfo;
    public List<Transform> destinations;
    Transform nextTransform;
    private bool switched = true;
    public Transform origin;
    public GameObject og;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        nav.destination = destinations[0].position;
        destinations.RemoveAt(0);
        nav.stoppingDistance = 4f;
        nav.speed = 0.1f;
        // nextTransform = destinations[0];
    }

    public void moveToDestination(Transform destination)
    {
        destinations.Add(destination);
    }

    void Update()
    {
        //if reached destination 
        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            if (destinations.Count != 0)
            {
                nav.destination = destinations[0].position;
                destinations.RemoveAt(0);
            }
            else
            {
                if (!switched)
                {
                    m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
                    anim.Play(m_CurrentClipInfo[0].clip.name, 0, 0.95f);
                    switched = true;
                }
                isMoving = false;
            }
        }
        // walking towards destination
        else
        {
            if (switched)
            {
                m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
                anim.Play(m_CurrentClipInfo[0].clip.name, 0, 0.95f);
                switched = false;
            }
            isMoving = true;
        }

    }


}
