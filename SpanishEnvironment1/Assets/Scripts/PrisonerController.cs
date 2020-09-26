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
        nav.speed = 0.5f;
        nav.stoppingDistance = 0.3f;
    }
    IEnumerator LineUp()
    {
        anim.SetBool("Walking", true);
        yield return new WaitForSeconds(3f);
        anim.SetBool("Sitting", false);
        yield return new WaitForSeconds(2f);
        m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        anim.Play(m_CurrentClipInfo[0].clip.name, 0, 0.95f);
        startMoving = true;
    }

    void Update()
    {
        if (Input.GetKey("i"))
        {
            m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
            anim.Play(m_CurrentClipInfo[0].clip.name, 0, 0.95f);
            if (mobile)
            {
                door.GetComponent<Animator>().Play("door open", 0, 0f);
                door.GetComponent<AudioSource>().Play();
                StartCoroutine(LineUp());
            }
        }
        if (startMoving)
        {

            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                if (destinations.Count != 0)
                {
                    nav.destination = destinations[0].position;
                    destinations.RemoveAt(0);

                }
                else
                {
                    anim.SetBool("Walking", false);
                }

            }
        }
    }

}
