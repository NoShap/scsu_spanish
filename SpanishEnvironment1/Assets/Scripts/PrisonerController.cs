using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PrisonerController : MonoBehaviour
{
    private Animator anim;
    AnimatorClipInfo[] m_CurrentClipInfo;
    public GameObject door;
    private NavMeshAgent nav;
    public List<Transform> destinations;
    public bool mobile;
    private bool startMoving = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }
    IEnumerator LineUp()
    {
        yield return new WaitForSeconds(6f);
        startMoving = true;
    }

    void Update()
    {
        if (Input.GetKey("i"))
        {
            // m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
            // anim.Play(m_CurrentClipInfo[0].clip.name, 0, 0.95f);
            if (mobile)
            {
                anim.SetBool("Sitting", false);
                door.GetComponent<Animator>().Play("door open", 0, 0f);
                door.GetComponent<AudioSource>().Play();
                StartCoroutine(LineUp());
            }
        }
        print(startMoving);
        if (startMoving)
        {
            anim.SetBool("Walking", true);
            while (destinations.Count != 0)
            {
                nav.destination = destinations[0].position;
                if (nav.stoppingDistance <= nav.remainingDistance)
                {
                    print("remove Destination: " + nav.remainingDistance + nav.stoppingDistance);
                    destinations.RemoveAt(0);
                }
            }
        }
    }

}
