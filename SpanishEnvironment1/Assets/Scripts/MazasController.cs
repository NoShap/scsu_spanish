using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class MazasController : MonoBehaviour
{
    public List<Transform> destinations;
    private Animator anim;
    private NavMeshAgent nav;
    public GameObject door;
    private bool doorOpened;
    private bool up;
    AnimatorClipInfo[] m_CurrentClipInfo;

    // Start is called before the first frame update
    void Start()
    {
        doorOpened = false;
        up = false;
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        // nav.destination = destinations[0].position;
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {

        // progress
        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            if (anim.GetBool("Walking"))
            {
                print("reached spot walking");
                print("Remaining:" + nav.remainingDistance);
                print("Stopping:" + nav.stoppingDistance);
                destinations.RemoveAt(0);
                anim.SetBool("Walking", false);
                nav.Stop();
            }
        }

        // move Sanchez Mazas
        if (Input.GetKey("m"))
        {
            if (!doorOpened && up)
            {
                doorOpened = true;
                door.GetComponent<Animator>().Play("door open", 0, 0f);
                door.GetComponent<AudioSource>().Play();
            }
            if (doorOpened)
            {
                anim.SetBool("Walking", true);
                nav.destination = destinations[0].position;
                nav.Resume();
                FindObjectOfType<AudioManager>().Play("Move");
                //Fetch the current Animation clip information for the base layer
                m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
                anim.Play(m_CurrentClipInfo[0].clip.name, 0, 0.95f);
            }
        }

        if (Input.GetKey("g"))
        {
            up = true;
            anim.SetBool("Sitting", false);
            FindObjectOfType<AudioManager>().Play("Get");
        }
        if (Input.GetKey("t"))
        {
            FindObjectOfType<AudioManager>().Play("Tree");
        }
        if (Input.GetKey("c"))
        {
            destinations.RemoveAt(0);
            if (destinations[0] != null)
            {
                nav.destination = destinations[0].position;
            }
        }

    }
}
